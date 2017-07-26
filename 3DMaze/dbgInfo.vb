Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

Public Class dbgInfo
    Inherits DrawableGameComponent
    Private _fps As Integer
    Private _time As Double
    Public Shared debug As String = "W, S, A, D: Movement" & vbLf & "Space: Jump" & vbLf & "Shift: Run" & vbLf & "Q: Jet-Pack" & vbLf & "RMB: Zoom-in" & vbLf & "F1: Solid" & vbLf & "F2: Wireframe" & vbLf & "F3: Player camera" & vbLf & "F4: Free camera" & vbLf & "F11: Randomize level" & vbLf & "F12: Free no-clip camera" & vbLf & "ESC: quit"
    Private font As SpriteFont
    Private fps As Integer
    Private spriteBatch As SpriteBatch

    Public Sub New(game As Game)
        MyBase.New(game)
    End Sub

    Public Overrides Sub Draw(gameTime As GameTime)
        Me._fps += 1
        Me._time += gameTime.ElapsedGameTime.TotalSeconds
        While Me._time > 1.0
            Me.fps = Me._fps
            Me._fps = 0
            Me._time -= 1
        End While
        MyBase.Draw(gameTime)
        Me.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullCounterClockwise)
        Dim str As String = String.Concat(New Object() {"FPS: ", Me.fps, vbLf, debug})
        Me.spriteBatch.DrawString(Me.font, str, Vector2.One, Color.Black)
        Me.spriteBatch.DrawString(Me.font, str, Vector2.Zero, Color.Red)
        Me.spriteBatch.End()
    End Sub

    Public Overrides Sub Initialize()
        MyBase.Initialize()
        Me.spriteBatch = New SpriteBatch(MyBase.GraphicsDevice)
    End Sub

    Protected Overrides Sub LoadContent()
        MyBase.LoadContent()
        Me.font = MyBase.Game.Content.Load(Of SpriteFont)("dbg")
    End Sub
End Class
