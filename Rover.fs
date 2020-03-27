module Rover

open System.IO

type CoordinateUnit = uint8

let CoordinateUnit = uint8

type X = CoordinateUnit

type Y = CoordinateUnit

type Coordinates = X * Y

type Direction =
    | North
    | East
    | South
    | West

type Rover =
    { Coordinates: Coordinates
      Direction: Direction }

let createRover (coordinates: Coordinates) (direction: Direction): Rover =
    { Coordinates = coordinates
      Direction = direction }

type MovingCommand =
    | Forward
    | Backward

type TurningCommand =
    | ClockWise
    | CounterClockWise

type Command =
    | MoveForward
    | MoveBackward
    | TurnClockWise
    | TurnCounterClockWise


let move (direction: Direction) (coordinates: Coordinates) (movingCommand: MovingCommand): Coordinates =
    let x, y = coordinates
    match direction with
    | North ->
        match movingCommand with
        | Forward -> (x, y + CoordinateUnit 1)
        | Backward -> (x, y - CoordinateUnit 1)
    | East ->
        match movingCommand with
        | Forward -> (x + CoordinateUnit 1, y)
        | Backward -> (x - CoordinateUnit 1, y)
    | South ->
        match movingCommand with
        | Forward -> (x, y - CoordinateUnit 1)
        | Backward -> (x, y + CoordinateUnit 1)
    | West ->
        match movingCommand with
        | Forward -> (x - CoordinateUnit 1, y)
        | Backward -> (x + CoordinateUnit 1, y)

let turn (direction: Direction) (turiningCommand: TurningCommand): Direction =
    match turiningCommand with
    | ClockWise ->
        match direction with
        | North -> East
        | East -> South
        | South -> West
        | West -> North
    | CounterClockWise ->
        match direction with
        | North -> West
        | East -> North
        | South -> East
        | West -> South

let commandRover (rover: Rover) (command: Command): Rover =
    match command with
    | MoveForward -> { rover with Coordinates = move rover.Direction rover.Coordinates Forward }
    | MoveBackward -> { rover with Coordinates = move rover.Direction rover.Coordinates Backward }
    | TurnClockWise -> { rover with Direction = turn rover.Direction ClockWise }
    | TurnCounterClockWise -> { rover with Direction = turn rover.Direction CounterClockWise }

let printCoordinates (tw: TextWriter) (x, y) = tw.Write("X:{0} Y:{1}", x, y)

let printRover (tw: TextWriter) (rover: Rover) =
    let x, y = rover.Coordinates
    tw.Write("Rover:(Coordinates=(X:{0} Y:{1}) ; Direction={2})", x, y, rover.Direction)
