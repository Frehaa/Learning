module Sampler

[<AbstractClass>]
type Sampler =
    new : unit -> Sampler
    abstract member GetSample : unit -> (float * float) list