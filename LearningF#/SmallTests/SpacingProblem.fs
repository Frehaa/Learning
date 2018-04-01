let no = 3
let s = 1. / (float no + 1.)

    // Returns a list of regular sample-points with n * n samples
    let rec c' rowN cols a = 
        match cols with 
        | n when n < 1 -> a    
        | n -> c' rowN (n-1) ((s * float rowN, s * float n)::a)
    and r' n a = 
        match n with
        | n when n < 1 -> a
        | n -> r' (n-1) ((c' n no [])@a)

r' no []

// We want 2 -> [(0.33, 0.33);(0.66, 0.33);(0.33, 0.66);(0.66, 0.66);]
// 

let n = 0

match n with
| n when n < 1 -> raise (invalidArg "n" "The sample set size should be larger than 0")
| _ -> n * n

type Raise =
    class
        val _N : int

        new (n : int) = 
            if n < 1 then raise (invalidArg "n" "The sample set size should be larger than 0")
            {_N = n}
    end


let r = new Raise(1)        
r._N
