

���޹��� ScrollView ʹ��

�� Content �Ϲ� LoopScrollView �ű�
Content ·��Ϊ ScrollView/Viewport/Content


Ȼ�����ò���ֵ 
Left�����������������

Top������������Ϸ�����

CellSize��������ߴ�

Spacing����������

FixedRowCol: �̶���/�У���ֱ����ѡ��̶��У�ˮƽ����ѡ��̶���

FixedCount��ÿ��/�й̶��ĸ�������СΪ 1


�����������
// ��ȡ LoopScrollView ���
Transform contentTr = transform.Find("ScrollView/Viewport/Content")
LoopScrollView  _loopScrollView = contentTr .GetComponent<LoopScrollView>();

// ��ʼ������ÿһ��ˢ�µĺ�����ÿһ��������UI����ĸ�ֵ�� RefreshItem �ص�������
_loopScrollView.Init(RefreshItem);

// ��������������
_loopScrollView.ItemCount(130);


// �ڼ�����������ת�������������Ϸ�
int goToIndex = 10;
_loopScrollView.GoToIndex(goToIndex);


///  Cell ������ʾ����
/// �� ScrollView ��ʾ����� �Ϸ����·�����ߡ��ұߡ��м�(��ǰ��ʾ)
CellShowType cellShowType = _loopScrollView.GetCellShowType(50);


