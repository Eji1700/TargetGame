module Game

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Content

type TargetGame () as x =
    inherit Game()
 
    do x.Content.RootDirectory <- "Content"
    do x.IsMouseVisible <- true
    let graphics = new GraphicsDeviceManager(x)
    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>
    let mutable targetSprite = Unchecked.defaultof<Texture2D>
    let mutable crosshairsSprite = Unchecked.defaultof<Texture2D>
    let mutable backgroundSprite = Unchecked.defaultof<Texture2D>
    
    override x.Initialize() =
        spriteBatch <- new SpriteBatch(x.GraphicsDevice)
        base.Initialize()

    override this.LoadContent() =
        targetSprite <- this.Content.Load<Texture2D>("target") 
        crosshairsSprite <- this.Content.Load<Texture2D>("crosshairs") 
        backgroundSprite <- this.Content.Load<Texture2D>("sky") 
 
    override this.Update (gameTime) =

         // TODO: Add your update logic here
        
        ()
 
    override this.Draw (gameTime) =
        x.GraphicsDevice.Clear Color.CornflowerBlue
        spriteBatch.Begin()
        spriteBatch.Draw(targetSprite, Vector2(0f,0f), Color.White)
        spriteBatch.End()