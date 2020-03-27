// Learn more about F# at http://fsharp.org

open System
open Rover

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    let freshRover = createRover (CoordinateUnit 2, CoordinateUnit 3) (North)
    let commandList = [MoveForward; MoveForward; MoveBackward; TurnClockWise; MoveForward; MoveForward; TurnCounterClockWise; MoveForward]
    let rover = List.fold commandRover freshRover commandList
    printfn "%a" printRover rover
    0 // return an integer exit code
