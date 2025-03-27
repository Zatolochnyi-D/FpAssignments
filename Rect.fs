module FpAssignments.GUI.Rect
open FpAssignments.Utilities
open FpAssignments.Utilities.Vector

type Anchor = TopLeft | TopCenter | Center
type Rect = {
    position: Vector
    dimensions: Vector
    anchor: Anchor
    pivot: Anchor
}

let rectWithPosition rect x y = { rect with position = { x = x; y = y } }
let rectWithAlignment rect anchor pivot = { rect with anchor = anchor; pivot = pivot }

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