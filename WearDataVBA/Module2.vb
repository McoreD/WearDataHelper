Sub UpdatePictures()

    Dim pictureName As String 'picture name
    Dim dirPictures As String 'dir path of pictures

    Dim dirPicturesSubDir As String
    Dim strPumpName As String
    Dim strPumpGroupName As String
    Dim strDate As String
    Dim pathPumpPhoto As String
    Dim pathPicture As String 'file path of picture

    Dim rowPhotoName As Integer
    Dim colPhotoName As Integer

    Application.ScreenUpdating = False

    strDate = Year(Worksheets("Check List Report").Cells(6, 12).Value) & "-" & Format(Month(Worksheets("Check List Report").Cells(6, 12).Value), "00")
    strPumpName = ActiveSheet.Cells(6, 7).Value
    strPumpGroupName = ActiveSheet.Cells(7, 9).Value

    dirPicturesSubDir = strDate & "\" & strPumpName
    dirPictures = ActiveWorkbook.Path + "\Photos\" + dirPicturesSubDir + "\" 'important to end with a "\"

    pathPumpPhoto = ActiveWorkbook.Path + "\Config\" & strPumpGroupName & "\" & strPumpName & ".jpg"

    Call InsertPicture(pathPumpPhoto, ActiveSheet.Cells(26, 1))

    For rowPhotoName = 19 To 31 Step 6

        For colPhotoName = 6 To 11 Step 5

            pictureName = Worksheets("Check List Report").Cells(rowPhotoName, colPhotoName).Value

            If (pictureName <> vbNullString) Then

                pathPicture = dirPictures & pictureName & ".jpg"

                Dim rowPhoto As Integer
                rowPhoto = rowPhotoName + 1
                ActiveSheet.Cells(rowPhoto, colPhotoName).Select
                DeletePicture(Cells(rowPhoto, colPhotoName))

                Call InsertPicture(pathPicture, ActiveSheet.Cells(rowPhoto, colPhotoName))

            End If

        Next

    Next

    ImportCSV(dirPictures & strPumpName & ".csv")

Exit_Sub:

    Application.ScreenUpdating = True
    Exit Sub

    GoTo Exit_Sub

End Sub

Sub InsertPicture(pathPicture As String, target As Range)

    If FileExists(pathPicture) Then

        With ActiveSheet.Pictures.Insert(pathPicture)
            With .ShapeRange
                .LockAspectRatio = msoTrue
                .Height = 125
                .Rotation = 0
            End With
            .Left = target.Left
            .Top = target.Top
            .Placement = 1
            .PrintObject = True
        End With

    End If

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

Sub ImportCSV(filePath As String)

    Dim rFirstCell As Range 'Points to the First Cell in the row currently being updated
    Dim rCurrentCell As Range 'Points the the current cell in the row being updated
    Dim sCSV As String 'File Name to Import
    Dim iFileNo As Integer 'File Number for Text File operations
    Dim sLine As String 'Variable to read a line of file into
    Dim sValue As String 'Individual comma delimited value

    If Not FileExists(filePath) Then Exit Sub

    'Clear Existing Data
    Worksheets("CSV").Cells.ClearContents

'Set initial values for Range Pointers
    Set rFirstCell = Worksheets("CSV").Cells(1, 1)
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