import codecs

textfile = "chinesetext.utf8"
wordlist_file = "chinesetrad_wordlist.utf8"

with codecs.open(textfile, mode='r', encoding="utf8") as f, \
     codecs.open(wordlist_file, mode='r', encoding="utf8") as wl:
    
    words = set()
    max_word_length = 0
    for line in wl:
        word = line.strip()
        words.add(word.encode("utf8"))
        if len(word) > max_word_length:
            max_word_length = len(word)

    segmentations = []
    for line in f:
        segments = []

        line = line.strip()
        i = 0
        while i < len(line):
            length = min(len(line) - i, max_word_length)
            for j in range(length, 0, -1):
                word = line[i: i + j] 
                if j == 1:
                    segments.append(word)
                    i += 1
                    break
                if word.encode("utf8") in words:
                    segments.append(word)
                    i += j
                    break

            
        segmentations.append((' '.join(segments)))


print("\n".join(segmentations))

