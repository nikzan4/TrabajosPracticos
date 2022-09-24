Imports System.IO
Imports System.Threading
Public Class Form1
    Dim hiloEscrituraArchivo As Thread

    Private synchronizationContext As SynchronizationContext

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        synchronizationContext = SynchronizationContext.Current
    End Sub

    Private Sub escribirArchivo()
        File.Create("archivo.txt").Dispose()
        Dim sw As New StreamWriter("archivo.txt")
        Try
            Dim cantidad As UInteger = Integer.Parse(TextBox1.Text)
            Dim incremento As Double = 100 / cantidad
            Dim valor As Double = 0
            For index = 1 To Integer.Parse(TextBox1.Text)
                sw.WriteLine("Linea: {0}.", index)
                valor += incremento
                synchronizationContext.Send(New SendOrPostCallback(Sub()
                                                                       Label1.Text = ProgressBar1.Value & "%"
                                                                       ProgressBar1.Value = valor
                                                                       Label1.Refresh()
                                                                   End Sub), Nothing)
            Next
            MsgBox("Terminado!")
        Catch ex As Exception
            If TypeOf ex Is ThreadAbortException Then
                sw.Dispose()
                File.Delete("archivo.txt")
                MsgBox("Cancelado con exito!")
            Else
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al escribir archivo")
            End If
        Finally
            sw.Dispose()
            sw.Close()
            synchronizationContext.Send(New SendOrPostCallback(Sub()
                                                                   Label1.Text = 0 & "%"
                                                                   ProgressBar1.Value = 0
                                                                   Label1.Refresh()
                                                               End Sub), Nothing)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        hiloEscrituraArchivo = New Thread(AddressOf escribirArchivo) With {.IsBackground = True}
        hiloEscrituraArchivo.Start()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If hiloEscrituraArchivo.IsAlive Then
            hiloEscrituraArchivo.Abort()
        Else
            MsgBox("No hay nada que cancelar.")
        End If
    End Sub

End Class
