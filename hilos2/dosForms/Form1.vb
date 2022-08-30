Imports System.Threading
Public Class Form1
    Inherits System.Windows.Forms.Form
    <STAThread()>
    Public Shared Sub main()
        Dim hilo1 As New Thread(AddressOf Ventana1)
        Dim hilo2 As New Thread(AddressOf Ventana2)
        hilo1.Start()
        hilo2.Start()
    End Sub

    Public Shared Sub Ventana1()
        Application.Run(New Form1)
    End Sub
    Public Shared Sub Ventana2()
        Application.Run(New Form2)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Top = 220
        Left = 220
    End Sub
    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        Dim parar As Integer
        Do While parar < 1000
            Label1.Text = parar.ToString()
            parar += 1
        Loop
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub
End Class