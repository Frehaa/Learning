import sys
import nltk
import numpy as np
from collections import defaultdict

train_file = sys.argv[1]
test_file = sys.argv[2]

START_TAG = "<START>"
END_TAG = "<END>"
UNKNOWN = "<UNKNOWN>"

tags = set([START_TAG, END_TAG])
vocab = set([UNKNOWN])

# Side effect of adding tags to tag set
def file_to_tag_list(filename):
    sentences = []
    with open(filename, 'r', encoding='utf8') as f:
        sentence_tags = []
        for line in f:
            line = line.strip()
            if len(line) == 0: 
                sentences.append(sentence_tags)
                sentence_tags = []
            else:
                word, tag = line.split()
                tags.add(tag)
                vocab.add(word)
                sentence_tags.append((word, tag))
    return sentences

def train(train_sentences):
    tag_tag_count, tag_word_count = count_occurences(train_sentences)
    log_normalize(tag_tag_count, tag_word_count)
    return tag_word_count, tag_tag_count

def count_occurences(sentences):
    tag_tag_count = defaultdict(lambda: defaultdict(int))
    tag_word_count = defaultdict(lambda: defaultdict(int))
    for sentence_pairs in train_sentences:
        prev_tag = START_TAG
        for word, tag in sentence_pairs:
            tag_tag_count[prev_tag][tag] += 1
            tag_word_count[tag][word] += 1
            prev_tag = tag
        tag_tag_count[prev_tag][END_TAG] += 1 
    return tag_tag_count, tag_word_count

# Modifies dicts
def log_normalize(tag_tag_count, tag_word_count):
    for tag in tag_tag_count:
        word_count = sum(tag_word_count[tag].values())
        tag_count = sum(tag_tag_count[tag].values())
        for ntag, value in tag_tag_count[tag].items():
            tag_tag_count[tag][ntag] = value / tag_count
        for ntag in tags:
            tag_tag_count[tag][ntag] = np.log(tag_tag_count[tag][ntag])

        for word, value in tag_word_count[tag].items():
            tag_word_count[tag][word] = value / tag_count
        for word in vocab:
            tag_word_count[tag][word] = np.log(tag_word_count[tag][word])


def viterbi(sentence, tag_tag_prob, tag_word_prob):
    viterbi_matrix = defaultdict(lambda: defaultdict(int))
    backpointer = defaultdict(lambda: {})
    states = tags.difference([START_TAG, END_TAG])

    for tag in states:
        viterbi_matrix[0][tag] = tag_tag_prob[START_TAG][tag] +\
                                    tag_word_prob[tag][sentence[0]]
        backpointer[0][tag] = START_TAG

    for t in range(1, len(sentence)):
        word = sentence[t]
        for tag in states:
            def calc_score(prev_tag):
                prev_score = viterbi_matrix[t-1][prev_tag]
                transition = tag_tag_prob[prev_tag][tag]
                word_score = tag_word_prob[tag][word]
                score = prev_score + transition + word_score
                return score

            best_score, best_tag = arg_max(states, calc_score)

            viterbi_matrix[t][tag] = best_score
            backpointer[t][tag] = best_tag
         
    def calc_score(prev_tag):
        prev_score = viterbi_matrix[len(sentence)-1][prev_tag]
        transition = tag_tag_prob[prev_tag][END_TAG]
        score = prev_score + transition
        return score
    
    best_score, best_tag = arg_max(states, calc_score)
    backpointer[len(sentence)][END_TAG] = best_tag

    return recover_path(backpointer, len(sentence))

def arg_max(states, func):
    best_score = float('-inf')
    best_tag = None
    for s in states:
        score = func(s)
        if score >= best_score:
            best_score = score
            best_tag = s

    return best_score, best_tag

def recover_path(backpointer, length):
    path = []
    prev_tag = END_TAG
    for t in range(length, 0, -1):
        prev_tag = backpointer[t][prev_tag]
        path.append(prev_tag)
    path.reverse()
    return path

train_sentences = file_to_tag_list(train_file)

wtp, ttp = train(train_sentences)

sentence = []
s_tags = []
for word, tag in train_sentences[0]:
    sentence.append(word)
    s_tags.append(tag)

print(viterbi(sentence, ttp, wtp))
