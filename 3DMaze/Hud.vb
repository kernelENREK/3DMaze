Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

Public Class Hud
    Inherits DrawableGameComponent
    Private crosshair As Texture2D
    Private crosshairPos As Vector2
    Private spriteBatch As SpriteBatch

    Public Sub New(game As Game)
        MyBase.New(game)
    End Sub

    Public Overrides Sub Draw(gameTime As GameTime)
        MyBase.Draw(gameTime)
        Me.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullCounterClockwise)
        Me.spriteBatch.Draw(Me.crosshair, Me.crosshairPos, Color.White)
        Me.spriteBatch.End()
    End Sub

    Public Overrides Sub Initialize()
        MyBase.Initialize()
        Me.spriteBatch = New SpriteBatch(MyBase.GraphicsDevice)
    End Sub

    Protected Overrides Sub LoadContent()
        MyBase.LoadContent()
        Me.crosshair = MyBase.Game.Content.Load(Of Texture2D)("cross")
        Me.crosshairPos = New Vector2(CSng((MyBase.GraphicsDevice.Viewport.Width / 2) - (Me.crosshair.Width / 2)), CSng((MyBase.GraphicsDevice.Viewport.Height / 2) - (Me.crosshair.Height / 2)))
    End Sub

    Public Overrides Sub Update(gameTime As GameTime)
        MyBase.Update(gameTime)
    End Sub
End Class
