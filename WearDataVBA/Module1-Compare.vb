

Sub UpdatePictures()

    Dim pictureNameRow As Long 'row where picture name is found
    Dim picturePasteRow As Long 'row where picture is to be pasted
    Dim pictureName As String 'picture name

    Dim pictureColumn As Long 'current picture column to be processed
    Dim dirPictures As String 'dir path of pictures

    Dim rowPhotoName As Integer
    Dim colPhotoName As Integer

    Dim dirPicturesSubDir As String

    Dim pathPicture As String 'file path of picture

    pictureNameRow = 6
    picturePasteRow = 7
    pictureColumn = 2

    rowPhotoName = 19
    colPhotoName = 1

    Application.ScreenUpdating = False

    Worksheets("Check List Report").Activate

    For rowPhotoName = 19 To 49 Step 6

        For colPhotoName = 1 To 11 Step 5

            dirPicturesSubDir = Year(Worksheets("Check List Report").Cells(18, colPhotoName + 1).Value) & "-" &
            Format(Month(Worksheets("Check List Report").Cells(18, colPhotoName + 1).Value), "00") & "\" &
            Worksheets("Check List Report").Cells(9, 2).Value

            dirPictures = ActiveWorkbook.Path + "\Photos\" + dirPicturesSubDir + "\" 'important to end with a "\"

            pictureName = Worksheets("Check List Report").Cells(rowPhotoName, colPhotoName).Value

            If (pictureName <> vbNullString) Then

                pathPicture = dirPictures & pictureName & ".jpg"

                If FileExists(pathPicture) Then

                    Dim rowPhoto As Integer
                    rowPhoto = rowPhotoName + 1

                    ActiveSheet.Cells(rowPhoto, colPhotoName).Select

                    DeletePicture(Cells(rowPhoto, colPhotoName))

                    With ActiveSheet.Pictures.Insert(pathPicture)
                        With .ShapeRange
                            .LockAspectRatio = msoTrue
                            .Width = 33
                            .Height = 135
                            .Rotation = 0
                        End With
                        .Left = ActiveSheet.Cells(rowPhoto, colPhotoName).Left
                        .Top = ActiveSheet.Cells(rowPhoto, colPhotoName).Top
                        .Placement = 1
                        .PrintObject = True
                    End With

                End If

            End If


        Next

    Next


Exit_Sub:

    Cells(1, 1).Select
    Application.ScreenUpdating = True
    Exit Sub


    GoTo Exit_Sub



End Sub


Sub DeletePicture(curcell As Range)

    On Error GoTo Err_DeletePicture

    Dim sh As Shape
    For Each sh In ActiveSheet.Shapes
        If sh.TopLeftCell.Address = curcell.Address Then sh.Delete
    Next

Err_DeletePicture:
    Exit Sub
End Sub




