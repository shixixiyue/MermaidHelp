
//初始化
F.ready(() => {

    $('#rightPanel_Content').addClass('mermaid');

    mermaid.initialize({
        startOnLoad: true,
        securityLevel: 'loose',
    });

    // 设置代码面板文本并显示代码和视图
    F.ui.codePanel.setText = function (msg) {
        F.ui.codePanel.codeText = msg;
        const htmlContent = markedAPI(msg, mermaidAPI);
        document.getElementById('codePanel_Content').innerHTML = htmlContent;
        hljs.initLineNumbersOnLoad({ singleLine: true });
        F.ui.messagePanel.setText(F.ui.txtInput.getText());
    };
    F.ui.codePanel.mermaidText = "";
    F.ui.codePanel.mermaidPako = "";

    // 设置历史会话文本
    F.ui.messagePanel.setText = function (msg) {
        let msgdiv = $(`<div>${msg}</div>`).addClass("history");
        $(`#messagePanel_Content`).append(msgdiv);
        F.ui.txtInput.setText(``);
    };

    // 清除历史会话
    F.ui.messagePanel.clear = function () {
        $(`#messagePanel_Content`).html(``);
    };

});


//#region 页面事件
// 复制代码到剪贴板
async function btnCopy_Click() {
    try {
        await copyToClipboard(F.ui.codePanel.codeText);
    } catch (e) {
        console.error('复制失败:', e);
    }
}

// 打开 Mermaid 编辑器
function btnOpenMermaid_Click() {
    const dataURL = `https://mermaid.live/edit#pako:${F.ui.codePanel.mermaidPako}`;
    window.open(dataURL, '_blank');
}

// 导出图片
function btnSaveToImg_Click() {
    const dataURL = `https://mermaid.ink/img/pako:${F.ui.codePanel.mermaidPako}?type=png`;

    fetch(dataURL)
        .then(response => response.blob())
        .then(blob => {
            const link = document.createElement('a');
            link.download = '导出图片.png';
            link.href = URL.createObjectURL(blob);
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
            URL.revokeObjectURL(link.href);
        })
        .catch(error => console.error('下载图片时出错:', error));
}

function btnMaxView_Click() {
    F.ui.rightPanel.maxView = !F.ui.rightPanel.maxView;
}
//#endregion 页面事件

//#region 私有方法


// 显示代码和 Mermaid 图表
const markedAPI = (msg, mermaidfun) => {
    return marked.use({
        renderer: {
            code(code, type) {
                if (type === "mermaid") mermaidfun(code);
                return `<pre><code class="hljs language-${type}">${code}</code></pre>`;
            }
        }
    })(msg);
};

// 渲染 Mermaid 图表
const mermaidAPI = async (code) => {
    F.ui.codePanel.mermaidText = code;
    F.ui.codePanel.mermaidPako = GetPako(code);
    const { svg } = await mermaid.render('mermaidSvg', code);
    setTimeout(() => {
        document.querySelector(`#rightPanel_Content`).innerHTML = svg;
    });
};

// 获取 Pako 压缩的代码
function GetPako(code) {
    return serialize(JSON.stringify({
        code,
        mermaid: { theme: "default" },
        autoSync: true,
        updateDiagram: true,
        editorMode: "code"
    }));
}

// 序列化和反序列化函数
function serialize(state) {
    const data = new TextEncoder().encode(state);
    const compressed = pako.deflate(data, { level: 9 });
    return Base64.fromUint8Array(compressed, true);
}

function deserialize(state) {
    const data = Base64.toUint8Array(state);
    return pako.inflate(data, { to: 'string' });
}

F.ready(() => {
    F.ui.rightPanel.__maxView = false;
    Object.defineProperty(F.ui.rightPanel, "maxView", {
        /**
         * @param {boolean} val
         */
        set(val) {
            if (val) {
                F.ui.toolMax.setText("恢复");
                F.ui.leftPanel.hide();
            } else {
                F.ui.toolMax.setText("最大化");
                F.ui.leftPanel.show();
            }
            F.ui.leftPanel.doLayout();
            F.ui.rightPanel.doLayout();
            this.__maxView = val;
        },
        get() {
            return this.__maxView;
        }
    });
})
//#endregion 私有方法