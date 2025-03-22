module FpAssignments.GUI
open System.Collections.Generic
open SimpleMath
open Colors

type Anchor = TopLeft | TopCenter

type DrawRect = {
    position: int * int
    dimensions: int * int
    anchor: Anchor
    pivot: Anchor
    content: List<List<char>>
    colorMap: List<List<Color>>
}

let rectTopLeftPosition drawRect =
    let width, height = drawRect.dimensions
    match drawRect.pivot with
        | TopLeft -> drawRect.position
        | TopCenter -> -(center width), 0

let createDrawRect pivot anchor position dimensions content colorMap= 
    { position = position; dimensions = dimensions; anchor = anchor; pivot = pivot; content = content; colorMap = colorMap}
