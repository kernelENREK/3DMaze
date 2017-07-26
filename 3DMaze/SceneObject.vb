Imports Microsoft.Xna.Framework

Public Class SceneObject
    Protected scene As Scene

    Public collide As Boolean = True
    Public onGround As Boolean
    Public visible As Boolean = True

    Public lastMove As Vector3
    Public move As Vector3 = Vector3.Zero
    Public position As Vector3 = Vector3.Zero

    Public Sub New(scene As Scene)
        Me.scene = scene
    End Sub

    Private Function CanMove(pos As Vector3, mv As Vector3, factor As Single, diagonal As Boolean) As Vector3
        Dim point As Vector3 = pos / 1000.0F
        Dim offset As Single = 0.0F
        If Not diagonal Then
            If (mv.X > 0.0F) AndAlso ((Me.CheckDistance(point, New Vector3(1.0F, 0.0F, 0.0F)) - offset) < mv.X) Then
                mv.X = Me.CheckDistance(point, New Vector3(1.0F, 0.0F, 0.0F)) - offset
            End If
            If (mv.X < 0.0F) AndAlso ((-Me.CheckDistance(point, New Vector3(-1.0F, 0.0F, 0.0F)) + offset) > mv.X) Then
                mv.X = -Me.CheckDistance(point, New Vector3(-1.0F, 0.0F, 0.0F)) + offset
            End If
            If (mv.Y > 0.0F) AndAlso ((Me.CheckDistance(point, New Vector3(0.0F, 1.0F, 0.0F)) - offset) < mv.Y) Then
                mv.Y = Me.CheckDistance(point, New Vector3(0.0F, 1.0F, 0.0F)) - offset
            End If
            If (mv.Y < 0.0F) AndAlso ((-Me.CheckDistance(point, New Vector3(0.0F, -1.0F, 0.0F)) + offset) > mv.Y) Then
                mv.Y = -Me.CheckDistance(point, New Vector3(0.0F, -1.0F, 0.0F)) + offset
                Me.onGround = True
            End If
            If (mv.Z > 0.0F) AndAlso ((Me.CheckDistance(point, New Vector3(0.0F, 0.0F, 1.0F)) - offset) < mv.Z) Then
                mv.Z = Me.CheckDistance(point, New Vector3(0.0F, 0.0F, 1.0F)) - offset
            End If
            If (mv.Z < 0.0F) AndAlso ((-Me.CheckDistance(point, New Vector3(0.0F, 0.0F, -1.0F)) + offset) > mv.Z) Then
                mv.Z = -Me.CheckDistance(point, New Vector3(0.0F, 0.0F, -1.0F)) + offset
            End If
        End If
        Dim direction As Vector3 = mv
        direction.Normalize()
        If diagonal Then
            Dim dst As Single = Me.CheckDistance(point, direction)
            If dst < mv.Length() Then
                mv = direction * dst
            End If
        End If
        Return mv
    End Function

    Private Function CheckDistance(point As Vector3, direction As Vector3) As Single
        Dim vector As Vector3 = point
        Dim maxCheck As Integer = 100
        direction.Normalize()
        direction = direction / CSng(maxCheck)
        For i As Integer = 0 To maxCheck - 1
            point += direction
            If point.X * 0 <> 0 Then
                point -= direction
                Exit For
            Else
                If Me.scene.level.IsBlock(CInt(Math.Round(CDbl(point.X))), CInt(Math.Round(CDbl(point.Y))), CInt(Math.Round(CDbl(point.Z)))) Then
                    point -= direction
                    Exit For
                End If

            End If
        Next
        Return (Vector3.Distance(point, vector) * 1000.0F)
    End Function

    Public Overridable Sub Draw(gameTime As GameTime)
    End Sub

    Public Overridable Sub Update(gameTime As GameTime)
        Dim factor As Single = CSng(gameTime.ElapsedGameTime.TotalMilliseconds) / 1000.0F
        Dim mv As Vector3 = Me.move * factor
        If Me.collide Then
            mv = Me.CanMove(Me.position + New Vector3(250.0F, 250.0F, 250.0F), mv, factor, False)
            mv = Me.CanMove(Me.position + New Vector3(-250.0F, 250.0F, 250.0F), mv, factor, False)
            mv = Me.CanMove(Me.position + New Vector3(250.0F, 250.0F, -250.0F), mv, factor, False)
            mv = Me.CanMove(Me.position + New Vector3(-250.0F, 250.0F, -250.0F), mv, factor, False)
            mv = Me.CanMove(Me.position + New Vector3(250.0F, -250.0F, 250.0F), mv, factor, False)
            mv = Me.CanMove(Me.position + New Vector3(-250.0F, -250.0F, 250.0F), mv, factor, False)
            mv = Me.CanMove(Me.position + New Vector3(250.0F, -250.0F, -250.0F), mv, factor, False)
            mv = Me.CanMove(Me.position + New Vector3(-250.0F, -250.0F, -250.0F), mv, factor, False)
            mv = Me.CanMove(Me.position + New Vector3(250.0F, 250.0F, 250.0F), mv, factor, True)
            mv = Me.CanMove(Me.position + New Vector3(-250.0F, 250.0F, 250.0F), mv, factor, True)
            mv = Me.CanMove(Me.position + New Vector3(250.0F, 250.0F, -250.0F), mv, factor, True)
            mv = Me.CanMove(Me.position + New Vector3(-250.0F, 250.0F, -250.0F), mv, factor, True)
            mv = Me.CanMove(Me.position + New Vector3(250.0F, -250.0F, 250.0F), mv, factor, True)
            mv = Me.CanMove(Me.position + New Vector3(-250.0F, -250.0F, 250.0F), mv, factor, True)
            mv = Me.CanMove(Me.position + New Vector3(250.0F, -250.0F, -250.0F), mv, factor, True)
            mv = Me.CanMove(Me.position + New Vector3(-250.0F, -250.0F, -250.0F), mv, factor, True)
        End If
        Me.position += mv
        Me.lastMove = mv
    End Sub
End Class
