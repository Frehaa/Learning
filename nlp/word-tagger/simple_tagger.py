import sys
from collections import defaultdict

train_file = sys.argv[1]
test_file = sys.argv[2]

sentences = []
tags = set()
with open(train_file, 'r', encoding='utf8') as file:
    sentence_tags = []
    for line in file:
        line = line.strip()

        if len(line) == 0: 
            sentences.append(sentence_tags)
            sentence_tags = []
        else:
            _, tag = line.split()
            tags.add(tag)
            sentence_tags.append(tag)

transition_matrix = defaultdict(lambda: defaultdict(int))

for sentence_tags in sentences:
    for i in range(1, len(sentence_tags)):
        t1, t2 = sentence_tags[i-1], sentence_tags[i]
        transition_matrix[t1][t2] += 1
        


for t1 in tags:
    for t2 in tags:
        print(f"{t1} -> {t2}: {transition_matrix[t1][t2]}")
