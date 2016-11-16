using UnityEngine;
using System;
using System.Collections;

public class WP8ProductTemplate  {



	private Texture2D _texture;

	public string ImgURL { get; set; }
	public string Name { get; set; }
	public string ProductId { get; set; }
	public string Price { get; set; }
	public WP8PurchaseProductType Type { get; set; }
	public string Description { get; set; }
	public bool isPurchased { get; set; }


	public event Action<Texture2D> ProdcutImageLoaded =  delegate {};

	
	public void LoadProductImage() {
		
		if(_texture != null) {
			ProdcutImageLoaded(_texture);
			return;
		}
		
		
		WPN_TextureLoader loader = WPN_TextureLoader.Create();
		loader.TextureLoaded += HandleTextureLoaded;
		loader.LoadTexture(ImgURL);
	}



	public Texture2D texture {
		get {
			return _texture;
		}
	}


	private void HandleTextureLoaded(Texture2D texture) {
		_texture = texture;
		ProdcutImageLoaded(_texture);

	}

}
