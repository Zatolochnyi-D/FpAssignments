module FpAssignments.GUI
open System.Collections.Generic
open System.Linq
open SimpleMath

type Anchor = TopLeft | TopCenter | Center
type Rect = {
    position: Vector
    dimensions: Vector
    anchor: Anchor
    pivot: Anchor
}

let setRectPosition rect x y = { rect with position = { x = x; y = y } }
let setRectAlignment rect anchor pivot = { rect with anchor = anchor; pivot = pivot }

type Fragment = {
    rect: Rect
    content: List<List<char>>
}

let fragment pivot anchor x y (content: List<List<char>>) =
    let dimensions = { x = content.[0].Count; y = content.Count }
    let position = { position = { x = x; y = y }; dimensions = dimensions; anchor = anchor; pivot = pivot }
    { rect = position; content = content }

let rectPivotShift rect =
    match rect.pivot with
        | TopLeft -> vector 0 0
        | TopCenter -> vector (center rect.dimensions.x |> (~-)) 0
        | Center -> vector (center rect.dimensions.x |> (~-)) (center rect.dimensions.y |> (~-))

let rectAbsolutePosition parentDimensions rect =
    let anchorShift = 
        match rect.anchor with
        | TopLeft -> vector 0 0
        | TopCenter -> vector (center parentDimensions.x) 0
        | Center -> vector (center parentDimensions.x) (center parentDimensions.y)
    let pivotShift = rectPivotShift rect
    anchorShift + pivotShift + rect.position

let setFragmentPosition fragment x y = { fragment with rect = setRectPosition fragment.rect x y }
let setFragmentAlignment fragment anchor pivot = { fragment with rect = setRectAlignment fragment.rect anchor pivot }

type TextAlignment = Left | Middle
type TextBox = {
    text: string
    alignment: TextAlignment
    fragment: Fragment
}

let alignLeft targetLength (string: string) =
    let emptyLength = targetLength - string.Length
    let empty = String.replicate emptyLength " "
    string + empty

let alignCenter targetLength (string: string) =
    let emptyLength = targetLength - string.Length
    let leftEmpty = String.replicate (emptyLength / 2 + emptyLength % 2) " "
    let rightEmpty = String.replicate (emptyLength / 2) " "
    leftEmpty + string + rightEmpty

let textBox pivot anchor alignment (text: string) =
    let lines = text.Split '\n'
    let biggestLength = lines.Max (fun x -> x.Length)
    let fragmentContent = List<List<char>> ()
    let alignFunc = 
        match alignment with
        | Left -> alignLeft
        | Middle -> alignCenter
    for line in lines do
        alignFunc biggestLength <| line |> charList |> fragmentContent.Add
    { text = text; alignment = alignment; fragment = fragment pivot anchor 0 0 fragmentContent }