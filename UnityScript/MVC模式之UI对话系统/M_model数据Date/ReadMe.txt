这是一个实体类
可串换化
Library是管理类
 拥有一个List<Dialog>
Dialog是一段对话的实体
 拥有一个List<Dialogitem>
Dialogitem是一段对话的某一句 
 拥有对话人名字 头像 内容

IDialogRegulation<T> 泛型接口 
 可以增加 返回指定的T
 删除指定的T
