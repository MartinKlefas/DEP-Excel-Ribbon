﻿Imports Microsoft.Office.Interop

Module AccountManagerEmail
    Public Sub Send_AM_Email(depLine As ClsDepLine)

        Dim templateFile As String = Environ("TEMP") & "\AM Email.oft"


        My.Computer.FileSystem.WriteAllBytes(templateFile, My.Resources.AM_Email, False)

        Dim AppOutlook As New Outlook.Application
        Dim amEmail As Outlook.MailItem
        amEmail = AppOutlook.CreateItemFromTemplate(templateFile)

        With amEmail
            .Subject = .Subject.Replace("%ordernum%", depLine.Sales_ID)
            .To = depLine.Account_Manager_Email

            .HTMLBody = .HTMLBody.Replace("%AM%", depLine.Account_Manager)
            .HTMLBody = .HTMLBody.Replace("ordernum", depLine.Sales_ID)
            .HTMLBody = .HTMLBody.Replace("ticketnum", depLine.NDT_Number)

            ' .Body = .Body.Replace("%AM%", depLine.Account_Manager)
            ' .Body = .Body.Replace("ordernum", depLine.Sales_ID)
            ' .Body = .Body.Replace("ticketnum", depLine.NDT_Number)


            .Display()
        End With
    End Sub
End Module
