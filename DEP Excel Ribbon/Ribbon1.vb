﻿Imports System.Diagnostics
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Tools.Ribbon
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome

Public Class Ribbon1

    Private Sub Ribbon1_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load
        If Not Globals.ThisAddIn.OnIntranet Then DisableButtons("It seems as though you're not currently connected to the intranet")
    End Sub

    Private Sub Button1_Click(sender As Object, e As RibbonControlEventArgs) Handles CreateNew.Click
        If Not Globals.ThisAddIn.RegistrationRunning Then
            Globals.ThisAddIn.RegistrationRunning = True
            Dim frm As New CreateNew(tDoAll:=True, showDebugInfo:=Me.ChkDebug.Checked)
            frm.Show()
        Else
            If MsgBox("It looks as though the registration process is already running. Would you like to interrupt the running process and start again?", vbYesNo) = vbYes Then
                Globals.ThisAddIn.RegistrationRunning = False
                Call Button1_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub DisableButtons(strMessage As String)
        Me.CreateNew.Enabled = False
        Me.FindIgnored.Enabled = False
        Me.CloseStale.Enabled = False
        Me.BtnCheckRegistrations.Enabled = False
        Me.CreateNew.SuperTip = strMessage
    End Sub

    Public Sub EnableButtons()
        Me.CreateNew.Enabled = True
        Me.FindIgnored.Enabled = True
        Me.CloseStale.Enabled = True
        Me.BtnCheckRegistrations.Enabled = True
        Me.CreateNew.SuperTip = "Create new nextdesk tickets based on the below DEP Spreadsheet"
    End Sub

    Private Sub Button2_Click(sender As Object, e As RibbonControlEventArgs) Handles CloseStale.Click
        Dim frm As New CloseStale
        frm.Show()
    End Sub



    Private Sub Button3_Click(sender As Object, e As RibbonControlEventArgs) Handles FindIgnored.Click
        Dim frm As New FindIgnored
        frm.Show()
    End Sub

    Function TrimClosed(txt As String) As String

        Dim list As String() = txt.ToLower.Split(vbCrLf)

        For i = 0 To list.Count - 1
            If list(i).ToLower.Contains("closed") Then
                Return Trim(list(i + 1).Split(" ")(0).Replace(vbLf, " "))
            End If
        Next
        Return ""
    End Function
    Function TrimClient(txt As String) As String

        Dim list As String() = txt.ToLower.Split(vbCrLf)

        For i = 0 To list.Count - 1
            If list(i).ToLower.Contains("client name") Then
                Return Trim(list(i + 1).Replace(vbLf, " "))
            End If
        Next
        Return ""
    End Function
    Function TrimAM(txt As String) As String

        Dim list As String() = txt.ToLower.Split(vbCrLf)

        For i = 0 To list.Count - 1
            If list(i).ToLower.Contains("description") Then
                Dim words = list(i + 1).Split(" ")
                For Each word In words
                    If word.Contains("@") Then
                        Return word
                    End If
                Next
            End If
        Next
        Return ""
    End Function

    Private Sub Button4_Click_1(sender As Object, e As RibbonControlEventArgs) Handles btnTestChrome.Click
        Dim options As New Chrome.ChromeOptions
        Dim service As ChromeDriverService = ChromeDriverService.CreateDefaultService


        MsgBox("trying to open a chrome window")


        Try
            Dim wd As New Chrome.ChromeDriver(service, options)
            wd.Navigate.GoToUrl("http://www.google.com")
        Catch
            MsgBox("oh dear, that didn't work at all!")
        End Try

    End Sub

    Private Sub WriteMails_Click(sender As Object, e As RibbonControlEventArgs) Handles WriteMails.Click
        Dim frm As New PivotMail
        frm.Show()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As RibbonControlEventArgs) Handles TDOnly.Click
        Dim frm As New CreateNew(tDoAll:=False, tDoTD:=True)
        frm.Show()
    End Sub

    Private Sub BtnWCOnly_Click(sender As Object, e As RibbonControlEventArgs) Handles BtnWCOnly.Click
        Dim frm As New CreateNew(tDoAll:=False, tDoWC:=True)
        frm.Show()
    End Sub

    Private Sub BtnCheckRegistrations_Click(sender As Object, e As RibbonControlEventArgs) Handles BtnCheckRegistrations.Click
        Dim frm As New CheckProgress(showDebugInfo:=Me.ChkDebug.Checked)
        frm.Show()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As RibbonControlEventArgs) Handles Button2.Click
        If Globals.ThisAddIn.OnIntranet Then
            EnableButtons()
        Else
            DisableButtons("It seems as though you're not currently connected to the intranet")
        End If
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As RibbonControlEventArgs) Handles Button3.Click
        MsgBox("Version from 18/06/19 16:23 - updated for multi-thread")
    End Sub

    Private Sub Button1_Click_2(sender As Object, e As RibbonControlEventArgs) Handles Button1.Click
        Dim ndt As New clsNextDeskTicket.ClsNextDeskTicket
        ndt.TicketNumber = 6571735
        Dim tmp = ndt.DEPScrape()
    End Sub
End Class
