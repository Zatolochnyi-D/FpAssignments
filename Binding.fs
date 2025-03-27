module FpAssignments.Window.Binding
open System

type Binding = {
    key: ConsoleKey
    func: unit -> unit
}