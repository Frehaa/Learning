let s = Seq.init 10 id

let odd = Seq.initInfinite (fun n -> n * 2 + 1)
Seq.item 5 odd

let oddFilter = Seq.filter (fun n -> n % 2 = 1) (Seq.initInfinite id)
Seq.item 0 oddFilter

let oddMap = Seq.map (fun n -> n * 2 + 1) (Seq.initInfinite id)
Seq.item 0 oddMap

let oddComplex = seq { for i in 0..10 do yield i }
Seq.item 10 oddComplex

for i in s do printf "%i\n" i

let printSeq = Seq.initInfinite (fun n -> printf "%i\n" n; n)
Seq.item 5 printSeq

let lazySeq = seq {for i in 1..10 do printf "%i\n" i; yield i}
Seq.item 0 lazySeq

let cons x sq = Seq.append (Seq.singleton x) sq

cons 5 Seq.empty

let rec from i = cons i (from(i+1))
// from 5  non-terminating

let rec from2 i = cons i (Seq.delay (fun () -> (from2(i+1))))
from2 5

let idWithPrint a = printf "%i\n" a ; a

idWithPrint 1

let delayed = Seq.delay (fun () -> Seq.init 5 idWithPrint)

let rec fromWithPrint2 i = Seq.delay (fun () -> cons (idWithPrint i) (fromWithPrint2(i+1)))
let from5 = (fromWithPrint2 5)
Seq.item 5 from5

let mapCons = Seq.map idWithPrint (cons 5 (seq [1;2;3]))
Seq.item 0 mapCons
Seq.item 3 mapCons

// Why does some sequences compute the whole sequence up to n, while other do not?????

let from5inf = Seq.initInfinite idWithPrint 
Seq.item 5 from5inf

let from5fin = Seq.init 50 idWithPrint 
Seq.item 5 from5fin