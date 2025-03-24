module FpAssignments.SimpleMath

open System

type Vector = { x: int; y: int }

let roundToInt (x: double) =
    x |> Math.Round |> int

let center sideSize =
    let shift = -1 + sideSize % 2
    sideSize / 2 + shift

let counter start = 
    let mutable count = start - 1
    fun () ->
        count <- count + 1
        count

// use 0 to get current sum
let sum () =
    let mutable previousSum = 0
    let mutable sum = 0
    fun addition ->
        previousSum <- sum
        sum <- sum + addition
        previousSum

let addTuples a b =
    let x1, y1 = a
    let x2, y2 = b
    x1 + x2, y1 + y2