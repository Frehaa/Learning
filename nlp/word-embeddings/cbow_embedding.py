import torch
import torch.nn as nn
import torch.nn.functional as F
import torch.optim as optim

EMBEDDING_DIM = 10
CONTEXT_SIZE = 2  # 2 words to the left, 2 to the right
raw_text = """We are about to study the idea of a computational process.
Computational processes are abstract beings that inhabit computers.
As they evolve, processes manipulate other abstract things called data.
The evolution of a process is directed by a pattern of rules
called a program. People create programs to direct processes. In effect,
we conjure the spirits of the computer with our spells.""".split()

# By deriving a set from `raw_text`, we deduplicate the array
vocab = set(raw_text)
vocab_size = len(vocab)

word_to_ix = {word: i for i, word in enumerate(vocab)}
data = []
for i in range(2, len(raw_text) - 2):
    context = [raw_text[i - 2], raw_text[i - 1],
               raw_text[i + 1], raw_text[i + 2]]
    target = raw_text[i]
    data.append((context, target))
# print(data[:5])


class CBOW(nn.Module):

    def __init__(self, vocab_size, embedding_dim, context_size):
        super(CBOW, self).__init__()
        self.embeddings = nn.Embedding(vocab_size, embedding_dim)
        self.bias = torch.randn(1, vocab_size)
        self.linear = nn.Linear(embedding_dim, vocab_size)

    def forward(self, inputs):
        embeds = self.embeddings(inputs)
        cbow = torch.sum(embeds, dim=0)
        A = self.linear(cbow)
        b = self.bias
        out = A + b
        log_probs = F.log_softmax(out, dim=1)
        return log_probs

# create your model and train.  here are some functions to help you make
# the data ready for use by your module


losses = []
loss_function = nn.NLLLoss()
model = CBOW(vocab_size, EMBEDDING_DIM, CONTEXT_SIZE)
optimizer = optim.SGD(model.parameters(), lr=0.001)

PATH = "./cbow_net.pth"
train = True
if train:
    for epoch in range(3000):
        total_loss = 0
        for context, target in data:
            
            context_idxs = torch.tensor([word_to_ix[w] for w in context],
                                                        dtype=torch.long)

            model.zero_grad()

            log_probs = model(context_idxs)

            loss = loss_function(log_probs, torch.tensor([word_to_ix[target]], 
                                                        dtype=torch.long))

            loss.backward()
            optimizer.step()

            total_loss += loss.item()
        losses.append(total_loss)

    print(losses)

    torch.save(model.state_dict(), PATH)

else:
    model.load_state_dict(torch.load(PATH))
    model.eval()

print(data[0][0])

context_idxs = torch.tensor([word_to_ix[w] for w in data[0][0]])
print(torch.argmax( model(context_idxs)) )
print(word_to_ix)
