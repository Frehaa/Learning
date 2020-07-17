import sys
from collections import defaultdict

train_file = sys.argv[1]
test_file = sys.argv[2]

START_TAG = "<START>"
END_TAG = "<END>"

tags = set([START_TAG, END_TAG])

# Side effect of adding tags to tag set
def file_to_tag_list(filename):
    sentences = []
    with open(filename, 'r', encoding='utf8') as f:
        sentence_tags = [START_TAG]
        for line in f:
            line = line.strip()

            if len(line) == 0: 
                sentence_tags.append(END_TAG)
                sentences.append(sentence_tags)
                sentence_tags = [START_TAG]
            else:
                _, tag = line.split()
                tags.add(tag)
                sentence_tags.append(tag)
    return sentences

def train(train_sentences):
    transition_matrix = defaultdict(lambda: defaultdict(int))
    for sentence_tags in train_sentences:
        for i in range(1, len(sentence_tags)):
            t1, t2 = sentence_tags[i-1], sentence_tags[i]
            transition_matrix[t1][t2] += 1
    return transition_matrix

def reduce_transition_matrix(matrix):
    for t1 in matrix:
        most_common = None
        common_count = 0
        for t2 in matrix[t1]:
            if matrix[t1][t2] > common_count:
                common_count = matrix[t1][t2]
                most_common = t2
        matrix[t1] = most_common
    return matrix

train_sentences = file_to_tag_list(train_file)
test_sentences = file_to_tag_list(test_file)

transition_matrix = train(train_sentences)

transition_matrix = reduce_transition_matrix(transition_matrix)

total = 0
correct = 0
for sentence in test_sentences:
    for i in range(1, len(sentence)):
        t1,t2 = sentence[i-1], sentence[i]
        total += 1
        if transition_matrix[t1] == t2:
            correct += 1

print(total, correct, correct / total)
