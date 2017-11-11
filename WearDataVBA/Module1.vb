Function FileExists(FileName As String) As Boolean
    FileExists = (Dir(FileName) > "")
End Function

Sub Expand()
    '
    ' Expand Macro
    '

    '
    ActiveSheet.Range("$A$2:$G$1031").AutoFilter Field:=1
    ActiveSheet.Range("$A$2:$G$1031").AutoFilter Field:=2, Criteria1:=
        "700077182"
    ActiveSheet.Range("$A$2:$G$1031").AutoFilter Field:=2
    ActiveSheet.Range("$A$2:$G$1031").AutoFilter Field:=3, Criteria1:=
        "---26T216N173"
    ActiveWindow.SmallScroll Down:=-15
    ActiveSheet.Range("$A$2:$G$1031").AutoFilter Field:=3
    ActiveSheet.Range("$A$2:$G$1031").AutoFilter Field:=4, Criteria1:=
        "700077126"
    ActiveSheet.Range("$A$2:$G$1031").AutoFilter Field:=4
    ActiveSheet.Range("$A$2:$G$1031").AutoFilter Field:=5, Criteria1:=
        "Cover Plate"
    ActiveSheet.Range("$A$2:$G$1031").AutoFilter Field:=5
    ActiveSheet.Range("$A$2:$G$1031").AutoFilter Field:=6, Criteria1:=
        "FRAME PLATE LINER INSERT"
    ActiveSheet.Range("$A$2:$G$1031").AutoFilter Field:=6
    ActiveWindow.SmallScroll Down:=-21
End Sub
Sub Macro4()
    '
    ' Macro4 Macro
    '

    '
    ActiveSheet.Range("$A$2:$G$1031").AutoFilter Field:=1, Criteria1:="17707/2"
    ActiveSheet.Range("$A$2:$G$1031").AutoFilter Field:=1
End Sub
Sub PRINTREPORT()
    '
    ' PRINTREPORT Macro
    '

    '
    ActiveWindow.SelectedSheets.PrintOut Copies:=1, Collate:=True,
        IgnorePrintAreas:=False
    Range("AC19").Select
    ActiveSheet.Shapes.Range(Array("TextBox 1")).Select
    Range("AC20").Select
    ActiveSheet.Shapes.Range(Array("TextBox 1")).Select
    Selection.Copy
    Range("AC19").Select
    Selection.ShapeRange.IncrementLeft 29.25
    Selection.ShapeRange.IncrementTop -12
    Selection.ShapeRange(1).TextFrame2.TextRange.Characters.Text =
        "Print Report - PDF"
    With Selection.ShapeRange(1).TextFrame2.TextRange.Characters(1, 18).
        ParagraphFormat
        .BaselineAlignment = msoBaselineAlignAuto
        .SpaceWithin = 1
        .SpaceBefore = 0
        .SpaceAfter = 0
        .IndentLevel = 1
        .FirstLineIndent = 0
        .HangingPunctuation = msoTrue
    End With
    With Selection.ShapeRange(1).TextFrame2.TextRange.Characters(1, 18).Font
        .BaselineOffset = 0
        .Bold = msoTrue
        .Caps = msoNoCaps
        .NameComplexScript = "+mn-cs"
        .NameFarEast = "+mn-ea"
        .Fill.Visible = msoTrue
        .Fill.ForeColor.RGB = RGB(112, 48, 160)
        .Fill.Transparency = 0
        .Fill.Solid
        .Size = 11
        .Italic = msoFalse
        .Kerning = 0
        .Line.Visible = msoFalse
        .Name = "Calibri"
        .Equalize = msoFalse
        .UnderlineStyle = msoNoUnderline
        .UnderlineColor
        .Spacing = 0
        .Strike = msoNoStrike
    End With
    Range("AC22").Select
    ActiveSheet.Shapes.Range(Array("TextBox 56")).Select
    Selection.ShapeRange.ScaleWidth 1.2907431551, msoFalse, msoScaleFromTopLeft
    Range("AB21").Select
    ActiveSheet.Shapes.Range(Array("TextBox 1")).Select
    Selection.ShapeRange.ScaleWidth 1.2398956975, msoFalse, msoScaleFromTopLeft
    Selection.ShapeRange.ScaleWidth 1.1356466877, msoFalse, msoScaleFromBottomRight
    Selection.ShapeRange(1).TextFrame2.TextRange.Characters.Text =
        "Print Report- hard copy"
    Selection.ShapeRange(1).TextFrame2.TextRange.Characters(1, 23).ParagraphFormat.
        FirstLineIndent = 0
    With Selection.ShapeRange(1).TextFrame2.TextRange.Characters(1, 18).Font
        .Bold = msoTrue
        .NameComplexScript = "+mn-cs"
        .NameFarEast = "+mn-ea"
        .Fill.Visible = msoTrue
        .Fill.ForeColor.RGB = RGB(112, 48, 160)
        .Fill.Transparency = 0
        .Fill.Solid
        .Size = 11
        .Name = "+mn-lt"
    End With
    With Selection.ShapeRange(1).TextFrame2.TextRange.Characters(19, 5).Font
        .BaselineOffset = 0
        .Bold = msoTrue
        .NameComplexScript = "+mn-cs"
        .NameFarEast = "+mn-ea"
        .Fill.Visible = msoTrue
        .Fill.ForeColor.RGB = RGB(112, 48, 160)
        .Fill.Transparency = 0
        .Fill.Solid
        .Size = 11
        .Name = "+mn-lt"
    End With
    Range("AB20").Select
    ActiveSheet.Shapes.Range(Array("TextBox 1")).Select
    Selection.ShapeRange.ScaleWidth 1.125, msoFalse, msoScaleFromBottomRight
    Range("AB21").Select
End Sub
Sub printpaper()
    '
    ' printpaper Macro
    '

    '
    ActiveWindow.SelectedSheets.PrintOut Copies:=1, Collate:=True,
        IgnorePrintAreas:=False
End Sub

Sub PrintPdf()
    '
    ' PrintPdf Macro
    ' Print the document in PDF
    '
    ' Keyboard Shortcut: Ctrl+Shift+P
    '
    ActiveWindow.SelectedSheets.PrintOut Copies:=1, Collate:=True,
        IgnorePrintAreas:=False

End Sub


