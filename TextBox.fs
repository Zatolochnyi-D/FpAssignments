module FpAssignments.GUI.TextBox
open System.Collections.Generic
open System.Linq
open FpAssignments.GUI.Fragment
open FpAssignments.Utilities

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