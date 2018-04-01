module Sampler

[<AbstractClass>]
type Sampler() =
    abstract member GetSample : unit -> (float * float) list