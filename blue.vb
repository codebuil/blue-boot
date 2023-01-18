Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel
Imports System.Globalization
Imports System.Diagnostics
Imports System.IO
Imports System.Collections.Generic
Imports System.Runtime.Serialization.Formatters.Binary


Public Class WinVBApp
    Inherits Form
        Dim button As New Button
        Dim text1 As New textbox
        Dim text2 As New textbox
        Dim text3 As New textbox
        Dim text4 As New textbox
        Dim label1 As New label
        Dim label2 As New label
        Dim label3 As New label
        Dim label4 As New label
        Dim label5 As New label

    Public Sub New

       Me.Text = "linesky@sapo.pt"
       Me.Size = New Size(640, 350)
       
       Me.InitUI
       
       Me.CenterToScreen

    End Sub
    
    Private Sub InitUI
    

        button.Location = New Point(280,30)
        button.Text = "boot image"
        button.Parent = Me

        text1.Location = New Point(5,20)
        text1.Text = "boot.asm"
        text1.Parent = Me

        text2.Location = New Point(5,70)
        text2.Text = "10"
        text2.Parent = Me

        text3.Location = New Point(5,120)
        text3.Text = "new.img"
        text3.Parent = Me

        text4.Location = New Point(5,170)
        text4.Text = "data"
        text4.Parent = Me

        label1.Location = New Point(5,0)
        label1.Text = " boot: "
        label1.Parent = Me

        label2.Location = New Point(5,50)
        label2.Text = " 143k blocks:"
        label2.Parent = Me

        label3.Location = New Point(5,100)
        label3.Text = " output image: "
        label3.Parent = Me

        label4.Location = New Point(5,150)
        label4.Text = " dir to imclude "
        label4.Parent = Me

        label5.Location = New Point(5,200)
        label5.size = new size(640,320-200)
        label5.Text = "  "
        label5.Parent = Me


        AddHandler button.Click, AddressOf Me.OnClick
        
        Me.CenterToScreen
        
    End Sub

    Private Sub OnClick(ByVal sender As Object, ByVal e As EventArgs)

		try           	
			dim psi as ProcessStartInfo = new ProcessStartInfo()
			psi.FileName = "nasm" 
			psi.UseShellExecute = false
			psi.Arguments = text1.Text + " -o BOOT.BIN" 
			psi.RedirectStandardOutput = true
			dim p as Process = Process.Start(psi)
			p = Process.Start(psi)
			label5.Text =p.StandardOutput.ReadToEnd()
			p.WaitForExit()
			p.Close()

			psi = new ProcessStartInfo()
			psi.FileName = "sudo" 
			psi.UseShellExecute = false
			psi.Arguments = "dd if=/dev/zero of="+text3.Text+" bs=143k count="+text2.text 
			psi.RedirectStandardOutput = true
			p = Process.Start(psi)
			label5.Text =p.StandardOutput.ReadToEnd()
			p.WaitForExit()
			p.Close()

			psi = new ProcessStartInfo()
			psi.FileName = "sudo" 
			psi.UseShellExecute = false
			psi.Arguments = "mkfs.vfat -n 'BLUE' -S 512 -f 2 -F 12 "+text3.Text
			psi.RedirectStandardOutput = true
			p = Process.Start(psi)
			label5.Text =p.StandardOutput.ReadToEnd()
			p.WaitForExit()
			p.Close()

			psi = new ProcessStartInfo()
			psi.FileName = "sudo" 
			psi.UseShellExecute = false
			psi.Arguments = "mkdir /mnt/new"
			psi.RedirectStandardOutput = true
			p = Process.Start(psi)
			label5.Text =p.StandardOutput.ReadToEnd()
			p.WaitForExit()
			p.Close()

			psi = new ProcessStartInfo()
			psi.FileName = "sudo" 
			psi.UseShellExecute = false
			psi.Arguments = "./dds BOOT.BIN "+text3.Text 
			psi.RedirectStandardOutput = true
			p = Process.Start(psi)
			label5.Text =p.StandardOutput.ReadToEnd()
			p.WaitForExit()
			p.Close()

			psi = new ProcessStartInfo()
			psi.FileName = "sudo" 
			psi.UseShellExecute = false
			psi.Arguments = "chmod 777 "+text3.Text 
			psi.RedirectStandardOutput = true
			p = Process.Start(psi)
			label5.Text =p.StandardOutput.ReadToEnd()
			p.WaitForExit()
			p.Close()


			psi = new ProcessStartInfo()
			psi.FileName = "sudo" 
			psi.UseShellExecute = false
			psi.Arguments = "mount -t vfat -o loop "+text3.Text +" /mnt/new"
			psi.RedirectStandardOutput = true
			p = Process.Start(psi)
			label5.Text =p.StandardOutput.ReadToEnd()
			p.WaitForExit()
			p.Close()


			psi = new ProcessStartInfo()
			psi.FileName = "sudo" 
			psi.UseShellExecute = false
			psi.Arguments ="sh -c 'cp ./"+text4.text+"/*.* /mnt/new'"
			psi.RedirectStandardOutput = true
			p = Process.Start(psi)
			label5.Text =p.StandardOutput.ReadToEnd()
			p.WaitForExit()
			p.Close()



			psi = new ProcessStartInfo()
			psi.FileName = "sudo" 
			psi.UseShellExecute = false
			psi.Arguments = "umount /mnt/new"
			psi.RedirectStandardOutput = true
			p = Process.Start(psi)
			label5.Text =p.StandardOutput.ReadToEnd()
			p.WaitForExit()
			p.Close()



			label5.Text =label5.Text+"finish"
                 catch ee as Exception 
			   label5.Text ="ERROR same data is not correct"
			   end try



    End Sub


    Public Shared Sub Main
        Application.Run(New WinVBApp)
    End Sub
   
End Class






