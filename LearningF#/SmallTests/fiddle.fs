
let mutable xs = [(0.25, 0.25);(0.50, 0.25);(0.75, 0.25);
                  (0.25, 0.50);(0.50, 0.50);(0.75, 0.50);
                  (0.25, 0.75);(0.50, 0.75);(0.75, 0.75);]

let n = 3

let sampleSet = 
    let s = 1. / (float n + 1.) // Spacing
    Seq.init (n * n) (fun index -> 
        let c = float(index % n + 1)
        let r = float(index / n + 1)
        (s * c, s * r)
    ) 
Seq.iter (fun (a, b) -> printfn "%A" (a, b)) sampleSet

Seq.fold (fun s (a, b) -> 
    let (a', b')::xs' = xs
    xs <- xs'
    s && (a, b) = (a', b')) true sampleSet

let mutable r = true

List.iter (fun x -> r <- r && Seq.contains x sampleSet) xs

let s = 0.5
let group = Seq.groupBy (fun (a, b) -> (int(a / s), int(b / s))) (Seq.ofList [(0.25, 0.25);(0.5, 0.25);(0.25, 0.55);(0.5, 0.5);])
Seq.length group

group


open System
let r = Random (4)
r.Next 5


let isMultipleOf d a = (a % d) = 0

isMultipleOf 5. 10.

(0.756176066 % 0.151235213) < 0.00000001

(0.756176065 % 0.151235213) - 0.151235213 < 0.00000001

10. % 5.


let isMultipleOf divisor significance a = 
        let rest : float = (a % divisor)
        rest < significance || abs(rest - divisor) < significance

isMultipleOf 0.5124123 0.000000001 (0.5124123 * 16.)

(0.2 % 0.5124123) - 0.5124123
