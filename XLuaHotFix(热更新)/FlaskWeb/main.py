from flask import Flask
from flask import request, make_response

app = Flask(__name__, static_folder='./AssetBundles')

# http://127.0.0.1:5000/AssetBundles/AssetBundles
# http://127.0.0.1:5000/AssetBundles/AssetBundles.manifest

# http://127.0.0.1:5000/AssetBundles/materials.u3d
# http://127.0.0.1:5000/AssetBundles/materials.manifest

# http://127.0.0.1:5000/AssetBundles/prefabs.u3d
# http://127.0.0.1:5000/AssetBundles/prefabs.u3d.manifest

# http://127.0.0.1:5000/AssetBundles/sprite.u3d
# http://127.0.0.1:5000/AssetBundles/sprite.u3d.manifest

@app.route('/',methods=['POST','GET'])
def hotfix():
    return "hello world"

if __name__ == '__main__':
    app.run()