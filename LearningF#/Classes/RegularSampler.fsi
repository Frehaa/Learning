module RegularSampler
open Sampler

[<Sealed>]
type RegularSampler = 
    inherit Sampler
    new : int -> RegularSampler    
    override GetSample : unit -> (float * float) list
