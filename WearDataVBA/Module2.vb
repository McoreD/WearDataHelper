Sub UpdatePictures()

    Dim strPumpName As String

    Dim dirPictures As String 'dir path of pictures
    Dim dirPicturesSubDir As String

    Const rowPhotoNameStart As Integer = 19
    Dim rowPhotoName As Integer
    Dim colPhotoName As Integer
    Dim pictureName As String 'picture name

    Dim pathPicture As String 'file path of picture

    rowPhotoName = 19
    colPhotoName = 1

    Application.ScreenUpdating = False

    Worksheets("Check List Report").Activate

    strPumpName = ActiveSheet.Cells(9, 2).Value
    Worksheets("CSV").Cells.ClearContents

    For rowPhotoName = 19 To 54 Step 7

        For colPhotoName = 1 To 11 Step 5

            dirPicturesSubDir = Year(Worksheets("Check List Report").Cells(18, colPhotoName + 1).Value) & "-" &
            Format(Month(Worksheets("Check List Report").Cells(18, colPhotoName + 1).Value), "00") & "\" &
            Worksheets("Check List Report").Cells(9, 2).Value

            dirPictures = ActiveWorkbook.Path + "\Photos\" + dirPicturesSubDir + "\" 'important to end with a "\"

            pictureName = Worksheets("Check List Report").Cells(rowPhotoName, colPhotoName).Value

            If (rowPhotoName = 19) Then ' Do only once

                Dim csvRow As Integer
                Select Case colPhotoName
                    Case 1
                        csvRow = 1
                    Case 6
                        csvRow = 11
                    Case 11
                        csvRow = 21
                End Select

                Call ImportCSV(dirPictures & strPumpName & ".csv", csvRow)

            End If

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
                            '.Width = 30
                            .Height = 132
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


Sub ImportCSV(filePath As String, rowFirstCell As Integer)

    Dim rFirstCell As Range 'Points to the First Cell in the row currently being updated
    Dim rCurrentCell As Range 'Points the the current cell in the row being updated
    Dim sCSV As String 'File Name to Import
    Dim iFileNo As Integer 'File Number for Text File operations
    Dim sLine As String 'Variable to read a line of file into
    Dim sValue As String 'Individual comma delimited value

    If Not FileExists(filePath) Then Exit Sub
        
'Set initial values for Range Pointers
    Set rFirstCell = Worksheets("CSV").Cells(rowFirstCell, 1)
    Set rCurrentCell = rFirstCell
    
'Get an available file number
    iFileNo = FreeFile()

    'Open your CSV file as a text file
    Open filePath For Input As #iFileNo

'Loop until reaching the end of the text file
    Do Until EOF(iFileNo)

        'Read in a line of text from the CSV file
        Line Input #iFileNo, sLine

        Do
            sValue = ParseData(sLine, ",")

            If sValue <> "" Then
                rCurrentCell = sValue 'put value into cell
                Set rCurrentCell = rCurrentCell.Offset(0, 1) 'move current cell one column right
            End If

        Loop Until sValue = ""
        
        Set rFirstCell = rFirstCell.Offset(1, 0) 'move pointer down one row
        Set rCurrentCell = rFirstCell 'set output pointer to next line
    Loop

    'Close the Text File
    Close #iFileNo
    
End Sub

Private Function ParseData(sData As String, sDelim As String) As String
    Dim iBreak As Integer

    iBreak = InStr(1, sData, sDelim, vbTextCompare)

    If iBreak = 0 Then
        If sData = "" Then
            ParseData = ""
        Else
            ParseData = sData
            sData = ""
        End If
    Else
        ParseData = Left(sData, iBreak - 1)
        sData = Mid(sData, iBreak + 1)
    End If

End Function

