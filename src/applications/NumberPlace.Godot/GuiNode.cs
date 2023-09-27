using Godot;
using Godot.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NumberPlace.Application.Abstracts;
using System;
using System.Threading.Tasks;

namespace NumberPlace.Godot
{
#nullable disable
    internal partial class GuiNode : Node
    {
        private bool _isInProgress = false;
        private IMatFactory _service;
        private ILogger<GuiNode> _logger;

        private Node3D _selectNode;
        private Node3D _progressNode;
        private Array<Node> _nodes;
        private MeshInstance3D _label;
        private BaseMaterial3D[] _materials = new BaseMaterial3D[10];
        private PrimitiveMesh[] _meshs = new PrimitiveMesh[9];
        private Mat[] _mats;   
        private int _index = 0;

        public static event Func<int> OnIndexBtnClicked;

        public override void _Ready()
        {
            _logger = NumberPlaceApplication.Instance.GetRequiredService<ILogger<GuiNode>>();
            _service = NumberPlaceApplication.Instance.GetRequiredService<IMatFactory>();
            _selectNode = GetNode<Node3D>("selectNode");
            _progressNode = GetNode<Node3D>("progressNode");
            _progressNode.Hide();
            _label = GetNode<MeshInstance3D>("progressNode/Label");
            _nodes = GetNode<Node>("/root/Node3D/numberPlace").GetChildren();

            for (int i = 0; i < 10; i++)
                _materials[i] = GD.Load<BaseMaterial3D>($"res://Mesh/surface_{i}.tres");

            for (int i = 0; i < 9; i++)
                _meshs[i] = GD.Load<PrimitiveMesh>($"res://Mesh/mesh_{i + 1}.tres");

            InputAreaNode.OnInputAreaChanged += OnEasyBtnClick;
            InputAreaNode.OnInputAreaChanged += OnMidBtnClick;
            InputAreaNode.OnInputAreaChanged += OnMasterBtnClick;
            InputAreaNode.OnInputAreaChanged += OnReturnBtnClick;
            InputAreaNode.OnInputAreaChanged += OnLeftBtnClick;
            InputAreaNode.OnInputAreaChanged += OnRightBtnClick;
        }

        private async void OnEasyBtnClick(object sender, string name)
        {
            if (name == "EasyArea3D" && !_isInProgress)
            {
                _selectNode.Hide();
                _progressNode.Show();
                _mats = await StartGenerateMat(10);
                ProcessMat();
                _isInProgress = true;
            }
        }

        private async void OnMidBtnClick(object sender, string name)
        {
            if (name == "MidArea3D" && !_isInProgress)
            {
                _selectNode.Hide();
                _progressNode.Show();
                _mats = await StartGenerateMat(20);
                ProcessMat();
                _isInProgress = true;
            }
        }

        private async void OnMasterBtnClick(object sender, string name)
        {
            if (name == "MasterArea3D" && !_isInProgress)
            {
                _selectNode.Hide();
                _progressNode.Show();
                _mats = await StartGenerateMat(30);
                ProcessMat();
                _isInProgress = true;
            }
        }

        private void OnReturnBtnClick(object sender, string name)
        {
            if (name == "ReturnArea3D" && _isInProgress)
            {
                _progressNode.Hide();
                _selectNode.Show();
                BackwardMat();
                _isInProgress = false;
            }
        }

        private void OnLeftBtnClick(object sender, string name)
        {
            if (name == "LeftArea3D")
            {
                if (_index == 0)
                    _index = 8;
                else
                    _index--;

                _label.Mesh = _meshs[_index];
                ProcessMat();
            }
        }

        private void OnRightBtnClick(object sender, string name)
        {
            if (name == "RightArea3D")
            {
                _index = (_index + 1) % 9;
                _label.Mesh = _meshs[_index];
                ProcessMat();
            }
        }

        private async Task<Mat[]> StartGenerateMat(int initCount)
        {
            Task<Mat>[] mats = new Task<Mat>[9];
            for (int i = 0; i < 9; i++)
                mats[i] = _service.GenerateMatAsync(initCount);
            return await Task.WhenAll(mats);
        }

        private void ProcessMat()
        {
            Mat mat = _mats[_index];
            for (int i = 0; i < 81; i++)
                ((MeshInstance3D)_nodes[i]).MaterialOverride = _materials[mat.Matrix[i]];
        }

        private void BackwardMat()
        {
            foreach (var item in _nodes)
                ((MeshInstance3D)item).MaterialOverride = _materials[0];

            _label.Mesh = _meshs[0];
        }
    }
}
