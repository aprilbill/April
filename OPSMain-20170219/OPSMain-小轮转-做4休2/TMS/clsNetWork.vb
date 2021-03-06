'Friend Class clsNetWork
'    '保持属性值的局部变量
'    Private mvarsLine As New cls_Lines '线路类集合
'    Private svarNetName As String '线网名称

'    '线路类集合
'    Public ReadOnly Property c_Line() As cls_Lines
'        Get
'            '检索属性值时使用，位于赋值语句的右边。
'            'Syntax: Debug.Print X.cls_lines
'            c_Line = mvarsLine
'        End Get
'    End Property

'    '线网名称
'    Public Property strNetName() As String
'        Get
'            Return svarNetName
'        End Get
'        Set(ByVal Value As String)
'            svarNetName = Value
'        End Set
'    End Property

'    '线路集合类*************************************************************************************************
'    Friend Class cls_Lines
'        Implements System.Collections.IEnumerable
'        '局部变量，保存集合
'        Private mCol As Collection

'        Public Function AddLine(ByRef sLineName As String, ByVal strEngName As String, ByVal strBrriName As String, ByRef nLong As Single, _
'        ByVal sLineNumber As String, ByVal strMemo As String, Optional ByRef sKey As String = "") As cls_Line


'            '创建新对象
'            Dim objNewMember As cls_Line
'            objNewMember = New cls_Line

'            '设置传入方法的属性

'            objNewMember.strName = sLineName
'            objNewMember.sngLength = nLong
'            objNewMember.strMemo = strMemo
'            objNewMember.strEngName = strEngName
'            objNewMember.strBrriName = strBrriName
'            objNewMember.sLineNumber = sLineNumber

'            If Len(sKey) = 0 Then
'                mCol.Add(objNewMember)
'            Else
'                mCol.Add(objNewMember, sKey)
'            End If


'            '返回已创建的对象
'            AddLine = objNewMember
'            'UPGRADE_NOTE: 在对对象 objNewMember 进行垃圾回收前，不可以销毁该对象。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"”
'            objNewMember = Nothing
'        End Function

'        Public Sub DeleteLine(ByVal intIndex As Integer) 'As cls_Line
'            mCol.Remove(intIndex)
'        End Sub


'        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cls_Line
'            Get
'                '引用集合中的一个元素时使用。
'                'vntIndexKey 包含集合的索引或关键字，
'                '这是为什么要声明为 Variant 的原因
'                '语法：Set foo = x.Item(xyz) or Set foo = x.Item(5)
'                Item = mCol.Item(vntIndexKey)
'            End Get
'        End Property



'        Public ReadOnly Property Count() As Integer
'            Get
'                '检索集合中的元素数时使用。语法：Debug.Print x.Count
'                Count = mCol.Count()
'            End Get
'        End Property


'        'UPGRADE_NOTE: NewEnum 属性已被注释掉。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1054"”
'        'Public ReadOnly Property NewEnum() As stdole.IUnknown
'        'Get
'        '本属性允许用 For...Each 语法枚举该集合。
'        'NewEnum = mCol._NewEnum
'        'End Get
'        'End Property

'        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
'            GetEnumerator = mCol.GetEnumerator
'        End Function


'        Public Sub Remove(ByRef vntIndexKey As Object)
'            '删除集合中的元素时使用。
'            'vntIndexKey 包含索引或关键字，这是为什么要声明为 Variant 的原因
'            '语法：x.Remove(xyz)
'            mCol.Remove(vntIndexKey)
'        End Sub


'        'UPGRADE_NOTE: Class_Initialize 已升级到 Class_Initialize_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"”
'        Private Sub Class_Initialize_Renamed()
'            '创建类后创建集合
'            mCol = New Collection
'        End Sub
'        Public Sub New()
'            MyBase.New()
'            Class_Initialize_Renamed()
'        End Sub


'        Private Sub Class_Terminate_Renamed()
'            '类终止后破坏集合
'            mCol = Nothing
'        End Sub
'        Protected Overrides Sub Finalize()
'            Class_Terminate_Renamed()
'            MyBase.Finalize()
'        End Sub
'    End Class

'    '线路类 #######################################################################
'    Friend Class cls_Line
'        '保持属性值的局部变量
'        Private mvarsName As String '线路名称
'        Private mvarnTracks As Short '股道数量
'        Private mvarnLength As Single '线路长度
'        Private mvarnEngName As String '英文名称
'        Private mvarnBrriName As String '线路简称
'        Private mvarnStaNumber As Integer  '车站总数
'        Private m_Memo As String '备注
'        Private m_Station As New cls_Stations '车站集合类


'        '车站类集合
'        Public Property c_Stations() As cls_Stations
'            Get
'                c_Stations = m_Station
'            End Get
'            Set(ByVal Value As cls_Stations)
'                m_Station = Value
'            End Set
'        End Property

'        '线路名属性
'        Public Property strName() As String
'            Get
'                '检索属性值时使用，位于赋值语句的右边。
'                'Syntax: Debug.Print X.sName
'                strName = mvarsName
'            End Get
'            Set(ByVal Value As String)
'                '向属性指派值时使用，位于赋值语句的左边。
'                'Syntax: X.sName = 5
'                mvarsName = Value
'            End Set
'        End Property

'        '线路长度属性
'        Public Property sngLength() As Single
'            Get
'                '检索属性值时使用，位于赋值语句的右边。
'                'Syntax: Debug.Print X.nLength
'                sngLength = mvarnLength
'            End Get
'            Set(ByVal Value As Single)
'                '向属性指派值时使用，位于赋值语句的左边。
'                'Syntax: X.nLength = 5
'                mvarnLength = Value
'            End Set
'        End Property

'        '线路车站数属性
'        Public Property sLineNumber() As Integer
'            Get
'                '检索属性值时使用，位于赋值语句的右边。
'                'Syntax: Debug.Print X.nLength
'                sLineNumber = mvarnStaNumber
'            End Get
'            Set(ByVal Value As Integer)
'                '向属性指派值时使用，位于赋值语句的左边。
'                'Syntax: X.nLength = 5
'                mvarnStaNumber = Value
'            End Set
'        End Property

'        '线路简称属性
'        Public Property strBrriName() As String
'            Get
'                '检索属性值时使用，位于赋值语句的右边。
'                'Syntax: Debug.Print X.nLength
'                strBrriName = mvarnBrriName
'            End Get
'            Set(ByVal Value As String)
'                '向属性指派值时使用，位于赋值语句的左边。
'                'Syntax: X.nLength = 5
'                mvarnBrriName = Value
'            End Set
'        End Property

'        '线路英文名属性
'        Public Property strEngName() As String
'            Get
'                '检索属性值时使用，位于赋值语句的右边。
'                'Syntax: Debug.Print X.nLength
'                strEngName = mvarnEngName
'            End Get
'            Set(ByVal Value As String)
'                '向属性指派值时使用，位于赋值语句的左边。
'                'Syntax: X.nLength = 5
'                mvarnEngName = Value
'            End Set
'        End Property



'        '线路备注属性
'        Public Property strMemo() As String
'            Get
'                '检索属性值时使用，位于赋值语句的右边。
'                'Syntax: Debug.Print X.nLength
'                strMemo = m_Memo
'            End Get
'            Set(ByVal Value As String)
'                '向属性指派值时使用，位于赋值语句的左边。
'                'Syntax: X.nLength = 5
'                m_Memo = Value
'            End Set
'        End Property


'        ''线路股道属性
'        'Public Property intTracks() As Short
'        '    Get
'        '        '检索属性值时使用，位于赋值语句的右边。
'        '        'Syntax: Debug.Print X.nTracks
'        '        intTracks = mvarnTracks
'        '    End Get
'        '    Set(ByVal Value As Short)
'        '        '向属性指派值时使用，位于赋值语句的左边。
'        '        'Syntax: X.nTracks = 5
'        '        mvarnTracks = Value
'        '    End Set
'        'End Property


'    End Class

'    '车站集合类 *************************************************************************************************
'    Friend Class cls_Stations
'        Implements System.Collections.IEnumerable
'        Private mCol As Collection

'        Public Function AddStation(ByVal nID As Integer, ByVal m_strName As String, _
'                                    ByVal m_intDownSeq As Integer, ByVal m_strStyle As String, _
'                                    ByVal m_strEnglName As String, ByVal m_strEnglAbbrName As String, _
'                                    ByVal m_strSingOrDoubLine As String, ByVal m_strCharacter As String, _
'                                    ByVal m_XCoordinate As Long, ByVal m_Ycoordinate As Long, _
'                                    ByVal m_StaPic As String, ByVal m_strMemo As String, Optional ByRef sKey As String = "") As cls_Station


'            '创建新对象
'            Dim objNewMember As cls_Station
'            objNewMember = New cls_Station


'            '设置传入方法的属性
'            objNewMember.intStaID = nID
'            objNewMember.strName = m_strName
'            objNewMember.intDownSeq = m_intDownSeq
'            objNewMember.strStyle = m_strStyle
'            objNewMember.strEnglName = m_strEnglName
'            objNewMember.strEnglAbbrName = m_strEnglAbbrName
'            objNewMember.strSingOrDoubLine = m_strSingOrDoubLine
'            objNewMember.strCharacter = m_strCharacter
'            objNewMember.XCoordinate = m_XCoordinate
'            objNewMember.YCoordinate = m_Ycoordinate
'            objNewMember.strPicName = m_StaPic
'            objNewMember.strMemo = m_strMemo

'            If Len(sKey) = 0 Then
'                mCol.Add(objNewMember)
'            Else
'                mCol.Add(objNewMember, sKey)
'            End If


'            '返回已创建的对象
'            AddStation = objNewMember
'            'UPGRADE_NOTE: 在对对象 objNewMember 进行垃圾回收前，不可以销毁该对象。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"”
'            objNewMember = Nothing


'        End Function

'        '删除车站
'        Public Sub DeleteStation(ByVal intIndex As Integer) 'As cls_Station
'            mCol.Remove(intIndex)
'        End Sub

'        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cls_Station
'            Get
'                '引用集合中的一个元素时使用。
'                'vntIndexKey 包含集合的索引或关键字，
'                '这是为什么要声明为 Variant 的原因
'                '语法：Set foo = x.Item(xyz) or Set foo = x.Item(5)
'                Item = mCol.Item(vntIndexKey)
'            End Get
'        End Property

'        Public ReadOnly Property Count() As Integer
'            Get
'                '检索集合中的元素数时使用。语法：Debug.Print x.Count
'                Count = mCol.Count()
'            End Get
'        End Property
'        Private Sub Class_Initialize_Renamed()
'            '创建类后创建集合
'            mCol = New Collection
'        End Sub
'        Public Sub New()
'            MyBase.New()
'            Class_Initialize_Renamed()
'        End Sub
'        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
'            'UPGRADE_TODO: 取消注释下行和更改下行以返回集合枚举数。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"”
'            GetEnumerator = mCol.GetEnumerator
'        End Function
'        'UPGRADE_NOTE: Class_Terminate 已升级到 Class_Terminate_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"”
'        Private Sub Class_Terminate_Renamed()
'            '类终止后破坏集合
'            'UPGRADE_NOTE: 在对对象 mCol 进行垃圾回收前，不可以销毁该对象。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"”
'            mCol = Nothing
'        End Sub
'        Protected Overrides Sub Finalize()
'            Class_Terminate_Renamed()
'            MyBase.Finalize()
'        End Sub

'    End Class

'    '车站类 #######################################################################
'    Friend Class cls_Station
'        Private m_staID As Integer '在路网中的ID号,唯一
'        Private m_strName As String '车站名称
'        Private m_intDownSeq As Integer  '下行站序
'        Private m_strStyle As String '车站类型
'        Private m_strEnglName As String '英文名称
'        Private m_strEnglAbbrName As String '英文缩写
'        Private m_strSingOrDoubLine As String '单双线
'        Private m_strCharacter As String '性质
'        Private m_XCoordinate As Long 'X坐标
'        Private m_Ycoordinate As Long 'Y坐标
'        Private m_StaPic As String '车站形图

'        Private m_strMemo As String '备注

'        '车站名称属性
'        Public Property strName() As String
'            Get
'                '检索属性值时使用，位于赋值语句的右边。
'                'Syntax: Debug.Print X.nNumber
'                strName = m_strName
'            End Get
'            Set(ByVal Value As String)
'                '向属性指派值时使用，位于赋值语句的左边。
'                'Syntax: X.nNumber = 5
'                m_strName = Value
'            End Set
'        End Property

'        '下行站序属性
'        Public Property intDownSeq() As Integer
'            Get
'                '检索属性值时使用，位于赋值语句的右边。
'                'Syntax: Debug.Print X.nNumber
'                intDownSeq = m_intDownSeq
'            End Get
'            Set(ByVal Value As Integer)
'                '向属性指派值时使用，位于赋值语句的左边。
'                'Syntax: X.nNumber = 5
'                m_intDownSeq = Value
'            End Set
'        End Property

'        '下行站序属性
'        Public Property intStaID() As Integer
'            Get
'                '检索属性值时使用，位于赋值语句的右边。
'                'Syntax: Debug.Print X.nNumber
'                intStaID = m_staID
'            End Get
'            Set(ByVal Value As Integer)
'                '向属性指派值时使用，位于赋值语句的左边。
'                'Syntax: X.nNumber = 5
'                m_staID = Value
'            End Set
'        End Property

'        '车站类型属性
'        Public Property strStyle() As String
'            Get
'                '检索属性值时使用，位于赋值语句的右边。
'                'Syntax: Debug.Print X.nNumber
'                strStyle = m_strStyle
'            End Get
'            Set(ByVal Value As String)
'                '向属性指派值时使用，位于赋值语句的左边。
'                'Syntax: X.nNumber = 5
'                m_strStyle = Value
'            End Set
'        End Property

'        '英文名称属性
'        Public Property strEnglName() As String
'            Get
'                strEnglName = m_strEnglName
'            End Get

'            Set(ByVal Value As String)
'                m_strEnglName = Value
'            End Set
'        End Property

'        '英文名称缩写属性
'        Public Property strEnglAbbrName() As String
'            Get
'                strEnglAbbrName = m_strEnglAbbrName
'            End Get

'            Set(ByVal Value As String)
'                m_strEnglAbbrName = Value
'            End Set
'        End Property

'        '单双线属性
'        Public Property strSingOrDoubLine() As String
'            Get
'                strSingOrDoubLine = m_strSingOrDoubLine
'            End Get

'            Set(ByVal Value As String)
'                m_strSingOrDoubLine = Value
'            End Set
'        End Property

'        '性质属性
'        Public Property strCharacter() As String
'            Get
'                strCharacter = m_strCharacter
'            End Get

'            Set(ByVal Value As String)
'                m_strCharacter = Value
'            End Set
'        End Property


'        'X坐标属性
'        Public Property XCoordinate() As Long
'            Get
'                XCoordinate = m_XCoordinate
'            End Get

'            Set(ByVal Value As Long)
'                m_XCoordinate = Value
'            End Set
'        End Property

'        'Y坐标属性
'        Public Property YCoordinate() As Long
'            Get
'                YCoordinate = m_Ycoordinate
'            End Get

'            Set(ByVal Value As Long)
'                m_Ycoordinate = Value
'            End Set
'        End Property

'        '车站形图属性
'        Public Property strPicName() As String
'            Get
'                strPicName = m_StaPic
'            End Get

'            Set(ByVal Value As String)
'                m_StaPic = Value
'            End Set
'        End Property


'        '备注属性  m_strMemo As String '备注
'        Public Property strMemo() As String
'            Get
'                strMemo = m_strMemo
'            End Get

'            Set(ByVal Value As String)
'                m_strMemo = Value
'            End Set
'        End Property

'    End Class


'    '股道集合类*************************************************************************************************
'    Friend Class cls_Tracks
'        Implements System.Collections.IEnumerable
'        '局部变量，保存集合
'        Private mCol As Collection

'        Public Function Add(ByRef nNumber As Short, Optional ByRef sKey As String = "") As cls_Track
'            '创建新对象
'            Dim objNewMember As cls_Track
'            objNewMember = New cls_Track


'            '设置传入方法的属性
'            objNewMember.intNumber = nNumber
'            If Len(sKey) = 0 Then
'                mCol.Add(objNewMember)
'            Else
'                mCol.Add(objNewMember, sKey)
'            End If


'            '返回已创建的对象
'            Add = objNewMember
'            'UPGRADE_NOTE: 在对对象 objNewMember 进行垃圾回收前，不可以销毁该对象。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"”
'            objNewMember = Nothing

'        End Function

'        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cls_Track
'            Get
'                '引用集合中的一个元素时使用。
'                'vntIndexKey 包含集合的索引或关键字，
'                '这是为什么要声明为 Variant 的原因
'                '语法：Set foo = x.Item(xyz) or Set foo = x.Item(5)
'                Item = mCol.Item(vntIndexKey)
'            End Get
'        End Property

'        Public ReadOnly Property Count() As Integer
'            Get
'                '检索集合中的元素数时使用。语法：Debug.Print x.Count
'                Count = mCol.Count()
'            End Get
'        End Property

'        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
'            'UPGRADE_TODO: 取消注释下行和更改下行以返回集合枚举数。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"”
'            GetEnumerator = mCol.GetEnumerator
'        End Function


'        Public Sub Remove(ByRef vntIndexKey As Object)
'            '删除集合中的元素时使用。
'            'vntIndexKey 包含索引或关键字，这是为什么要声明为 Variant 的原因
'            '语法：x.Remove(xyz)

'            mCol.Remove(vntIndexKey)
'        End Sub


'        'UPGRADE_NOTE: Class_Initialize 已升级到 Class_Initialize_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"”
'        Private Sub Class_Initialize_Renamed()
'            '创建类后创建集合
'            mCol = New Collection
'        End Sub
'        Public Sub New()
'            MyBase.New()
'            Class_Initialize_Renamed()
'        End Sub


'        'UPGRADE_NOTE: Class_Terminate 已升级到 Class_Terminate_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"”
'        Private Sub Class_Terminate_Renamed()
'            '类终止后破坏集合
'            'UPGRADE_NOTE: 在对对象 mCol 进行垃圾回收前，不可以销毁该对象。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"”
'            mCol = Nothing
'        End Sub
'        Protected Overrides Sub Finalize()
'            Class_Terminate_Renamed()
'            MyBase.Finalize()
'        End Sub
'    End Class


'    '股道类#######################################################################
'    Friend Class cls_Track
'        Private mvarnNumber As Short '局部复制


'        Public Property intNumber() As Short
'            Get
'                '检索属性值时使用，位于赋值语句的右边。
'                'Syntax: Debug.Print X.nNumber
'                intNumber = mvarnNumber
'            End Get
'            Set(ByVal Value As Short)
'                '向属性指派值时使用，位于赋值语句的左边。
'                'Syntax: X.nNumber = 5
'                mvarnNumber = Value
'            End Set
'        End Property
'    End Class

'End Class