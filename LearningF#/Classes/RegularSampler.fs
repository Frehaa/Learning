module RegularSampler
open Sampler

[<Sealed>]
type RegularSampler(n : int) = 
    inherit Sampler()
    
    override this.GetSample () = [(0.5, 0.5)]