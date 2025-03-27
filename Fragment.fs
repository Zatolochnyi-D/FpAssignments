module FpAssignments.GUI.Fragment
open System.Collections.Generic
open FpAssignments.GUI.Rect
open FpAssignments.Utilities.Vector

type Fragment = {
    rect: Rect
    content: List<List<char>>
}

let fragment pivot anchor x y (content: List<List<char>>) =
    let dimensions = { x = content.[0].Count; y = content.Count }
    let position = { position = { x = x; y = y }; dimensions = dimensions; anchor = anchor; pivot = pivot }
    { rect = position; content = content }

let setFragmentPosition fragment x y = { fragment with rect = rectWithPosition fragment.rect x y }
let setFragmentAlignment fragment anchor pivot = { fragment with rect = rectWithAlignment fragment.rect anchor pivot }