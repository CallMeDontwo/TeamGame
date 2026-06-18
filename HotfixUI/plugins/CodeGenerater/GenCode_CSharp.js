"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.genCode = void 0;
const csharp_1 = require("csharp");
const CodeWriter_1 = require("./CodeWriter");
function genCode(handler) {
    let settings = handler.project.GetSettings("Publish").codeGeneration;
    let codePkgName = handler.ToFilename(handler.pkg.name); //convert chinese to pinyin, remove special chars etc.
    let exportCodePath = handler.exportCodePath + "\\" + codePkgName;
    let namespaceName = codePkgName;
    let isMonoGame = handler.project.type == csharp_1.FairyEditor.ProjectType.MonoGame;
    if (settings.packageName)
        namespaceName = settings.packageName + '.' + namespaceName;
    //CollectClasses(stripeMemeber, stripeClass, fguiNamespace)
    let classes = handler.CollectClasses(settings.ignoreNoname, false, null);
    handler.SetupCodeFolder(exportCodePath, "cs"); //check if target folder exists, and delete old files
    let getMemberByName = settings.getMemberByName;
    let classCnt = classes.Count;
    let writer = new CodeWriter_1.default();
    for (let i = 0; i < classCnt; i++) {
        let classInfo = classes.get_Item(i);
        let members = classInfo.members;
        writer.reset();
        writer.writeln('using System.Threading.Tasks;');
        writer.writeln('using FairyGUI;');
        writer.writeln('using FairyGUI.Utils;');
        writer.writeln();
        writer.writeln('namespace %s', namespaceName);
        writer.startBlock();
        writer.writeln('[EnableClass]');
        writer.writeln('public partial class %s : %s', classInfo.className, classInfo.superClassName);
        writer.startBlock();
        writer.writeln('public const string URL = "ui://%s%s";', handler.pkg.id, classInfo.resId);
        writer.writeln('public const string UIResName = "%s";', classInfo.resName);
        writer.writeln('public const string UIPackageName = "%s";', handler.pkg.name);
        writer.writeln();
        let memberCnt = members.Count;
        for (let j = 0; j < memberCnt; j++) {
            let memberInfo = members.get_Item(j);
            writer.writeln('public %s %s { get; private set; }', memberInfo.res == null ? memberInfo.type : handler.ToFilename(memberInfo.res.owner.name) + "." + memberInfo.res.name, memberInfo.varName);
        }
        writer.writeln();
        writer.writeln('public static %s CreateInstance()', classInfo.className);
        writer.startBlock();
        writer.writeln('return (%s)UIPackage.CreateObject(UIPackageName, UIResName);', classInfo.className);
        writer.endBlock();
        writer.writeln();
        writer.writeln('public static Task<%s> CreateInstanceAsync()', classInfo.className);
        writer.startBlock();
        writer.writeln('TaskCompletionSource<%s> tcs = new TaskCompletionSource<%s>();', classInfo.resName, classInfo.resName);
        writer.writeln('UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as %s));', classInfo.className);
        writer.writeln('return tcs.Task;');
        writer.endBlock();
        writer.writeln();
        writer.writeln('public static %s GetFromPool(GObject gObject)', classInfo.className);
        writer.startBlock();
        writer.writeln('return (%s)gObject;', classInfo.className);
        writer.endBlock();
        writer.writeln();
        if (isMonoGame) {
            writer.writeln("protected override void OnConstruct()");
            writer.startBlock();
        }
        else {
            writer.writeln('public override void ConstructFromXML(XML xml)');
            writer.startBlock();
            writer.writeln('base.ConstructFromXML(xml);');
            writer.writeln();
        }
        writer.writeln("name = UIResName;");
        for (let j = 0; j < memberCnt; j++) {
            let memberInfo = members.get_Item(j);
            if (memberInfo.group == 0) {
                if (getMemberByName)
                    writer.writeln('%s = (%s)GetChild("%s");', memberInfo.varName, memberInfo.res == null ? memberInfo.type : handler.ToFilename(memberInfo.res.owner.name) + "." + memberInfo.res.name, memberInfo.name);
                else
                    writer.writeln('%s = (%s)GetChildAt(%s);', memberInfo.varName, memberInfo.res == null ? memberInfo.type : handler.ToFilename(memberInfo.res.owner.name) + "." + memberInfo.res.name, memberInfo.index);
            }
            else if (memberInfo.group == 1) {
                if (getMemberByName)
                    writer.writeln('%s = GetController("%s");', memberInfo.varName, memberInfo.name);
                else
                    writer.writeln('%s = GetControllerAt(%s);', memberInfo.varName, memberInfo.index);
            }
            else {
                if (getMemberByName)
                    writer.writeln('%s = GetTransition("%s");', memberInfo.varName, memberInfo.name);
                else
                    writer.writeln('%s = GetTransitionAt(%s);', memberInfo.varName, memberInfo.index);
            }
        }
        writer.writeln();
        writer.writeln("this.AfterCreate();");
        writer.endBlock();
        writer.writeln();
        writer.writeln("partial void AfterCreate();");
        writer.endBlock(); //class
        writer.endBlock(); //namepsace
        writer.save(exportCodePath + '/' + classInfo.className + '.cs');
    }
    let binderName = codePkgName + 'Binder';
    writer.reset();
    writer.writeln('using FairyGUI;');
    writer.writeln();
    writer.writeln('namespace %s', namespaceName);
    writer.startBlock();
    writer.writeln('[PackageBinder]');
    writer.writeln('public class %s : Object, IPackageBinder', binderName);
    writer.startBlock();
    writer.writeln('public void BindAll()');
    writer.startBlock();
    for (let i = 0; i < classCnt; i++) {
        let classInfo = classes.get_Item(i);
        writer.writeln('UIObjectFactory.SetPackageItemExtension(%s.URL, typeof(%s));', classInfo.className, classInfo.className);
    }
    writer.endBlock(); //bindall
    writer.endBlock(); //class
    writer.endBlock(); //namespace
    writer.save(exportCodePath + '/' + binderName + '.cs');
    let packageBinderName = 'Package' + codePkgName;
    writer.reset();
    writer.writeln('namespace %s', settings.packageName);
    writer.startBlock();
    writer.writeln('public static partial class FUIPackage');
    writer.startBlock();
    writer.writeln('public const string %s = "%s";', codePkgName, handler.pkg.name);
    for (let i = 0; i < handler.items.Count; i++) {
        const packageItem = handler.items.get_Item(i);
        const tempCodeName = packageItem.name.replace(/-/g, "_");
        if (packageItem.branch.length === 0) {
            writer.writeln('public const string %s_%s = "ui://%s/%s";', codePkgName, tempCodeName, handler.pkg.name, packageItem.name);
        }
    }
    writer.endBlock(); //class
    writer.endBlock(); //namespace
    writer.save(exportCodePath + '/' + packageBinderName + '.cs');
}
exports.genCode = genCode;
