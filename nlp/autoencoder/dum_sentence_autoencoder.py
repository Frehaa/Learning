# Simple 1 hot representation for each word. Append word vectors for length 10
# long senctence eg. Encode into single matrix. Decode

import numpy as np
from torch import relu,device, sigmoid, zeros
from torch.utils.data import DataLoader
from torch.nn import Linear, MSELoss, Module, CrossEntropyLoss, Softmax
from torch.optim import Adam, SGD

def file_to_tag_list(filename):
    sentences = []
    with open(filename, 'r', encoding='utf8') as f:
        words = []
        for line in f:
            line = line.strip()

            if len(line) == 0: 
                while len(words) < sentence_length:
                    words.append("")
                sentences.append(words[:sentence_length])
                words = []
            else:
                word, _ = line.split()
                vocab.add(word)
                words.append(word)
    return sentences

vocab = set([""])
sentence_length = 10

train_sentences = file_to_tag_list("train.conllu")
test_sentences = file_to_tag_list("test.conllu")

w2i = {w:(i+1) for i,w in enumerate(vocab)}
w2i[""] = 0

i2w = {i:w for w,i in w2i.items() } 

class AE(Module):
  def __init__(self, vocab_size, sentence_length):
    super().__init__()
    features = vocab_size * sentence_length
    self.e_hidden = Linear(in_features=features,out_features=128)
    self.d_output = Linear(in_features=128, out_features=features)
    
  def forward(self, features):
    activation = self.e_hidden(features)
    activation = sigmoid(activation)
    activation = self.d_output(activation)
    return sigmoid(activation)

model = AE(len(vocab), sentence_length)

epochs = 10
optimizer = SGD(model.parameters(), lr=5e-4)
criterion = MSELoss()

for epoch in range(epochs):
  loss = 0
  for sentence in train_sentences:
    sentence = [w2i[w] for w in sentence]

    inputs = zeros(sentence_length, len(vocab))

    for i in range(len(sentence)):
        idx = sentence[i]
        inputs[i][idx] = 1
    inputs = inputs.view(1, -1)

    optimizer.zero_grad()

    outputs = model(inputs)

    train_loss = criterion(outputs, inputs)
    train_loss.backward()
    optimizer.step()

    loss += train_loss.item()

  loss = loss / len(train_loader)
  
  print("epoch : {}/{}, loss = {:.6f}".format(epoch+1, epochs, loss))

