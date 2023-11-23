﻿namespace Cascade
{
    public static partial class Utilities
    {
        public static SpriteEffects DirectionBasedSpriteEffects(this Entity entity)
            => entity.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        /// <summary>
        /// Sets <see cref="Main.graphics"/>' GraphicsDevice to the specified <see cref="RenderTarget2D"/> 
        /// and clears the device with a specified color.
        /// </summary>
        /// <param name="flushColor">The color to clear the <see cref="GraphicsDevice"/> with. 
        /// Defaults to <see cref="Color.Transparent"/> if no value is manually given. 
        /// In most cases, you will want this to stay as the mentioned default value.</param>
        public static void SwapToTarget(this RenderTarget2D renderTarget, Color? flushColor = null)
        {
            GraphicsDevice graphicsDevice = Main.graphics.GraphicsDevice;

            // Do nothing if we are in the menu, on a server or if any of the following variables are null.
            if (Main.gameMenu || Main.dedServ || graphicsDevice is null || renderTarget is null)
                return;

            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(flushColor ?? Color.Transparent);
        }

        /// <summary>
        /// The same as <see cref="SwapToTarget(RenderTarget2D, Color?)"/>, though accepts a <see cref="SmartRenderTarget"/> instance
        /// instead of just a <see cref="RenderTarget2D"/> instance.
        /// </summary>
        /// <param name="flushColor">The color to clear the <see cref="GraphicsDevice"/> with. 
        /// Defaults to <see cref="Color.Transparent"/> if no value is manually given. 
        /// In most cases, you will want this to stay as the mentioned default value.</param>
        public static void SwapToTarget(this SmartRenderTarget smartRenderTarget, Color? flushColor = null)
            => SwapToTarget(smartRenderTarget.RenderTarget, flushColor ?? Color.Transparent);

        public static void DrawTextureOnProjectile(this Projectile projectile, Color lightColor, float rotation, float scale, SpriteEffects spriteEffects = SpriteEffects.None, bool animated = false, Texture2D texture = null)
        {
            texture ??= TextureAssets.Projectile[projectile.type].Value;

            int individualFrameHeight = texture.Height / Main.projFrames[projectile.type];
            int currentYFrame = individualFrameHeight * projectile.frame;
            Rectangle rectangle = animated ?
                new Rectangle(0, currentYFrame, texture.Width, individualFrameHeight) :
                new Rectangle(0, 0, texture.Width, texture.Height);

            Vector2 origin = rectangle.Size() / 2f;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), lightColor, rotation, origin, scale, spriteEffects, 0);
        }

        public static void ApplyRancorMagicCircleShader(Texture2D texture, float opacity, float circularRotation, float directionRotation, int direction, Color startingColor, Color endingColor, BlendState blendMode)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, blendMode, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

            CalculatePerspectiveMatricies(out var viewMatrix, out var projectionMatrix);
            GameShaders.Misc["CalamityMod:RancorMagicCircle"].UseColor(startingColor);
            GameShaders.Misc["CalamityMod:RancorMagicCircle"].UseSecondaryColor(endingColor);
            GameShaders.Misc["CalamityMod:RancorMagicCircle"].UseSaturation(directionRotation);
            GameShaders.Misc["CalamityMod:RancorMagicCircle"].UseOpacity(opacity);
            GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["uDirection"].SetValue(direction);
            GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["uCircularRotation"].SetValue(circularRotation);
            GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["uImageSize0"].SetValue(texture.Size());
            GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["overallImageSize"].SetValue(texture.Size());
            GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["uWorldViewProjection"].SetValue(viewMatrix * projectionMatrix);
            GameShaders.Misc["CalamityMod:RancorMagicCircle"].Apply();
        }
    }
}

