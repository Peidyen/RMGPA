namespace RMGPA.Core

type Location =
    | Grid of int * int
    | Vector3 of float * float * float
    | Tag of string
