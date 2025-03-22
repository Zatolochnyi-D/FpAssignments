module FpAssignments.GUI
open System.Collections.Generic
open SimpleMath
open Colors

type Anchor = TopLeft | TopCenter

type DrawRect = {
    position: int * int
    dimensions: int * int
    content: List<List<char>>
    colorMap: List<List<Color>>
}

let createDrawRect position dimensions content colorMap= 
    { position = position; dimensions = dimensions; content = content; colorMap = colorMap}
