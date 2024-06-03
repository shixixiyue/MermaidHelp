
//初始化
F.ready(() => {
    $('#rightPanel_Content').addClass('mermaid');

    mermaid.initialize({
        securityLevel: 'loose',
    });
    mermaid.setParseErrorHandler((e) => {
        console.log('e>', e);
    })
    // 设置代码面板文本并显示代码和视图
    F.ui.codePanel.setText = function (msg) {
        F.ui.codePanel.codelist.push(msg);
        F.ui.messagePanel.setText(F.ui.txtInput.getText());
    };
    F.ui.codePanel.mermaidText = "";
    F.ui.codePanel.mermaidPako = "";
    F.ui.codePanel.codelist = [];

    // 设置历史会话文本
    F.ui.messagePanel.setText = function (msg) {
        let msgdiv = $(`<div>${msg}</div>`).addClass("history");
        $(`#messagePanel_Content`).append(msgdiv);
        F.ui.txtInput.setText(``);
        F.ui.messagePanel.trigger('change', $(".history").length);
    };

    // 清除历史会话
    F.ui.messagePanel.clear = function () {
        $(`#messagePanel_Content`).html(``);
    };

    F.ui.btnscale.scale = 2;
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
function btnSaveToImg_Click(type = 'jpeg') {
    let width = $('#mermaidSvg').width();
    let scale = F.ui.btnscale.scale || 2;
    width = width * scale;
    const dataURL = `https://mermaid.ink/img/pako:${F.ui.codePanel.mermaidPako}?type=${type}&width=${width}`;

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

function btnscale_Click() {
    F.ui.btnscale.scale += 2;
    if (F.ui.btnscale.scale >= 12) F.ui.btnscale.scale = 2;
    F.ui.btnscale.setText(`放大${F.ui.btnscale.scale}倍`);
}

/**
 *
 * @param {up,down,back} type
 */
function btnPageChange(type) {
    switch (type) {
        case 'up':
            F.ui.messagePanel.pageIndex--;
            break;
        case 'down':
            F.ui.messagePanel.pageIndex++;
            break;
        case 'back':
            //F.ui.messagePanel.pageIndex++;
            F.doPostBack({
                eventTarget: 'btnBack',
                eventArgument: 'click',
                url: '?handler=btnBack_Click',
                params: {
                    page: F.ui.messagePanel.pageIndex - 1
                }
            });
            $('.history.active').nextAll('.history').remove();
            F.ui.codePanel.codelist.splice(F.ui.messagePanel.pageIndex);
            break;
    }
    F.ui.messagePanel.trigger('change', F.ui.messagePanel.pageIndex);
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
    F.ui.rightPanel.parse = true;
    try {
        F.ui.rightPanel.parse = await mermaid.parse(code);
    }
    catch (e) {
        F.ui.rightPanel.parse = e;
    }
    if (F.ui.rightPanel.parse === true) {
        F.ui.codePanel.mermaidText = code;
        F.ui.codePanel.mermaidPako = GetPako(code);
        const { svg } = await mermaid.render('mermaidSvg', code);
        setTimeout(() => {
            document.querySelector(`#rightPanel_Content`).innerHTML = svg;
        });
    }
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
    F.ui.rightPanel.__parse = true;
    Object.defineProperty(F.ui.rightPanel, "parse", {
        /**
         * @param {boolean} val
         */
        set(val) {
            if (val === true) {
                $(`.errordiv`).hide().remove();
            } else {
                let errordiv = $(`<div class="errordiv"></div>`);
                errordiv.html("语法错误：<br/>" + val);
                $(`#rightPanel_Content`).append(errordiv);
            }
            this.__parse = val;
        },
        get() {
            return this.__parse;
        }
    });

    setTimeout(() => {
        $(`#dmermaidSvg`).remove();
    });
    setBntEnable();

    F.ui.messagePanel.on("change", (e, index) => {
        F.ui.messagePanel.pageIndex = index;
        setBntEnable();
        $('.history').eq(index - 1).addClass("active").siblings().removeClass("active");

        let msg = F.ui.codePanel.codelist[index - 1];
        F.ui.codePanel.codeText = msg;
        const htmlContent = markedAPI(msg, mermaidAPI);
        document.getElementById('codePanel_Content').innerHTML = htmlContent;
        hljs.initLineNumbersOnLoad({ singleLine: true });
    });
})
//#endregion 私有方法

//#region 翻页

function setBntEnable() {
    if ($('.history').length <= 1) {
        F.ui.btnUp.setEnabled(false);
        F.ui.btnDown.setEnabled(false);
        F.ui.btnBack.setEnabled(false);
        return;
    }
    console.log('F.ui.messagePanel.pageIndex >', F.ui.messagePanel.pageIndex);
    //判断当前的编号是否是最后一个
    let pageindex = GetPageIndex();

    if (pageindex >= $('.history').length) {
        F.ui.btnUp.setEnabled(true);
        F.ui.btnDown.setEnabled(false);
        F.ui.btnBack.setEnabled(false);
    } else if (pageindex < $('.history').length) {
        F.ui.btnDown.setEnabled(true);
        F.ui.btnBack.setEnabled(true);
    }
    if (pageindex == 1) {
        F.ui.btnUp.setEnabled(false);
    }
}
function GetPageIndex() {
    return F.ui.messagePanel.pageIndex || $('.history').length;
}
//#endregion 翻页