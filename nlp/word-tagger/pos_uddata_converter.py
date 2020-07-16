import sys
import os

in_file = sys.argv[1]

sentences = []
with open(in_file, "r", encoding = 'utf-8') as fi:
    sentence = []
    for line in fi:
        if line.startswith("#"): continue
        if line.isspace():
            sentences.append("\n".join(sentence))
            sentence = []
        else:
            split = line.split()
            word = split[1]
            label = split[3]
            if label == "_": continue
            sentence.append(split[1] + "\t" + split[3])

    if len(sentence) != 0: # Add last sentence if any
        sentences.append(sentence)

for sentence in sentences:
    print(sentence)
    print()
