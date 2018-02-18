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

