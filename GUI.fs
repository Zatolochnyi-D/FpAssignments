module FpAssignments.GUI
open System.Collections.Generic
open SimpleMath

type Fragment = {
    position: Vector
    dimensions: Vector
    content: List<List<char>>
}

let fragmentFromCharList x y (content: List<List<char>>) =
    let position = { x = x; y = y }
    let dimensions = { x = content.[0].Count; y = content.Count }
    { position = position; dimensions = dimensions; content = content }

// let drawRectFromContentAndColorMaps position (content: List<List<char>>) (foregroundColorMap: List<List<Color>>) (backgroungColorMap: List<List<Color>>) = 
//     let squares = List<List<Square>> ()
//     for y = 0 to content.Count - 1 do
//         List<Square> () |> squares.Add
//         for x = 0 to content.[y].Count - 1 do
//             squares.[y].Add { char = content.[y].[x]; fgColor = foregroundColorMap.[y].[x]; bgColor = backgroungColorMap.[y].[x] }
//     drawRectFromCharList position squares
