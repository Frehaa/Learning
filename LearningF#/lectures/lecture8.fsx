// Slide 9
// How can does it know that r1 is record of mutable int???
// Couldn't it just as well be immutable

// Records needs to have a type defined before use, so if we define a record type with a count field
// Any variable which satisfies that, will be of the defined type
type intRec = {mutable count : int; }
let r1 = {count = 1}

// If two record types are identical safe for the mutable keyword, the most recently defined type which matches will be used
type intRec2 = {count : int; }
let r2 = {count = 2}

// But it is still possible to specifically refer to the old type
let r3 : intRec = {count = 3}
