
Sub UpdatePictures()

    Dim pictureNameRow As Long 'row where picture name is found
    Dim picturePasteRow As Long 'row where picture is to be pasted
    Dim pictureName As String 'picture name
    Dim lastPictureColumn As Long 'last column in use where picture names are
    Dim pictureColumn As Long 'current picture column to be processed
    Dim dirPictures As String 'dir path of pictures

    Dim dirPicturesSubDir As String

    Dim pathPicture As String 'file path of picture

    pictureNameRow = 6
    picturePasteRow = 7
    pictureColumn = 2

    lastPictureColumn = 7 ' Worksheets("WearInfo").Cells(pictureNameRow, Columns.Count).End(xlToLeft).Column
    Application.ScreenUpdating = False

    Do While (pictureColumn <= lastPictureColumn)

        On Error GoTo Try_Next

        dirPicturesSubDir = Year(Worksheets("WearInfo").Cells(pictureNameRow - 3, pictureColumn).Value) & "-" &
        Format(Month(Worksheets("WearInfo").Cells(pictureNameRow - 3, pictureColumn).Value), "00") & "\" &
        Worksheets("WearInfo").Cells(pictureNameRow - 2, pictureColumn).Value

        dirPictures = ActiveWorkbook.Path + "\Photos\" + dirPicturesSubDir + "\" 'important to end with a "\"

        pictureName = Worksheets("WearInfo").Cells(pictureNameRow, pictureColumn).Value

        If (pictureName <> vbNullString) Then

            pathPicture = dirPictures & pictureName & ".jpg"

            If FileExists(pathPicture) Then

                Worksheets("WearInfo").Activate
                Worksheets("WearInfo").Cells(picturePasteRow, pictureColumn).Select
                DeletePicture(Cells(picturePasteRow, pictureColumn))
                ActiveSheet.Pictures.insert(pathPicture).Select

                With Selection
                    .Left = Cells(picturePasteRow, pictureColumn).Left
                    .Top = Cells(picturePasteRow, pictureColumn).Top
                    '.ShapeRange.LockAspectRatio = msoFalse
                    .ShapeRange.Height = 100.0#
                    .ShapeRange.Width = .TopLeftCell.Width
                    .ShapeRange.Rotation = 0#
                End With


            End If
        Else

        End If

Try_Next:
        pictureColumn = pictureColumn + 1

    Loop

    Worksheets("Check List Report").Activate

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





