(* 3.1 *)

type Meridian = AM | PM;;
let beforeTuple (hours1, minutes1, f1) (hours2, minutes2, f2) = 
    if f1 < f2 then true
    else if f2 < f1 then false
    else if hours1 < hours2 then true
    else if hours2 < hours1 then false
    else minutes1 < minutes2;;

beforeTuple (11,59,AM) (1,15,PM);;
beforeTuple (11,59,AM) (1,15,AM);;
beforeTuple (1,14,AM) (1,15,AM);;

type TimeOfDay = {meridian : Meridian; hours : int; minutes: int };;

let beforeRecord t1 t2 = t1 < t2;;

let t1 = {meridian = AM; hours = 11; minutes = 59};;
let t2 = {meridian = PM; hours = 1; minutes = 15};;

let t3 = {meridian = AM; hours = 1; minutes = 15};;

let t4 = {meridian = AM; hours = 1; minutes = 14};;

beforeRecord t1 t2;;
beforeRecord t1 t3;;
beforeRecord t4 t3;;

(* 3.2 *)

(* 3.3 *)

(* 3.4 *)

(* 3.5 *)

(* 3.6 *)

(* 3.7 *)
