module FpAssignments.GUI

open System.Collections.Generic
open SimpleMath

type Anchor = TopLeft | TopCenter
type DrawRect = {
    position: int * int
    anchor: Anchor
    pivot: Anchor
    content: List<string>
    dimensions: int * int
}

let createDrawRect pivot anchor position (content: List<string>) = 
    let dimensions = content.[0] |> String.length |> center, content.Count |> center
    { position = position; anchor = anchor; pivot = pivot; content = content; dimensions = dimensions }

let rectTopLeftCoordinate drawRect =
    match drawRect.pivot with
        | TopLeft -> drawRect.position
        | TopCenter -> -(center drawRect.content.[0].Length), -(center drawRect.content.Count)