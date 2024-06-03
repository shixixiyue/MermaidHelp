(function () {
	$.ajaxSetup({
		// ajax请求之前进行accountToken封装
		beforeSend: function (xhr) {
			if (window?.top?.hub?.id) {
				xhr.setRequestHeader("hubid", window.top.hub.id);
			}
		}
	});
	/**创建异步方法 */
	AsyncFunction = Object.getPrototypeOf(async function () { }).constructor;

	/**生成GUID */
	GetGuid = () => {
		const S4 = () => (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
		return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
	}

	/** 函数防抖
	* @param func 函数
	* @param wait 延迟执行毫秒数
	* @param immediate true 表立即执行，false 表非立即执行
	*/
	debounce = (func = function () { }, wait = 800, immediate = false) => {
		let timeout;
		return function () {
			let context = this;
			let args = arguments;

			if (timeout) clearTimeout(timeout);
			if (immediate) {
				var callNow = !timeout;
				timeout = setTimeout(() => {
					timeout = null;
				}, wait)
				if (callNow) func.apply(context, args)
			}
			else {
				timeout = setTimeout(function () {
					func.apply(context, args)
				}, wait);
			}
		}
	}
	/**异步防抖 注意这里找不到this！
	 * debounceP(async ()=> {});
	 * @param func 函数
	 * @param wait 延迟执行毫秒数
	 */
	debounceP = (func = function () { }, wait = 800) => {
		let timeout;
		return function () {
			let context = this;
			let args = arguments;

			if (timeout) clearTimeout(timeout);
			return new Promise((r) => {
				timeout = setTimeout(async function () {
					r(await func.apply(context, args))
				}, wait);
			})
		}
	}
	/**
	* @desc 函数节流
	* @param func 函数
	* @param wait 延迟执行毫秒数
	* @param type 1 表时间戳版，2 表定时器版
	*/
	throttle = (func, wait = 500, type = 2) => {
		if (type === 1) {
			let previous = 0;
		} else if (type === 2) {
			let timeout;
		}
		return function () {
			let context = this;
			let args = arguments;
			if (type === 1) {
				let now = Date.now();

				if (now - previous > wait) {
					func.apply(context, args);
					previous = now;
				}
			} else if (type === 2) {
				if (!timeout) {
					timeout = setTimeout(() => {
						timeout = null;
						func.apply(context, args)
					}, wait)
				}
			}
		}
	}
	/**
	* 数组筛选
	* @param {Object}} {item:{type:"",vla:""}}
	* @return {Array} []
	*/
	Array.prototype.search = function (searchdata) {
		let t = this;
		let typefn = {
			["包含"]: (item, val) => item.includes(val),
			["开始"]: (item, val) => item.startsWith(val),
			["结束"]: (item, val) => item.endsWith(val),
			["等于"]: (item, val) => item == val,
			["不等于"]: (item, val) => item != val,
			["大于"]: (item, val) => item > val,
			["小于"]: (item, val) => item < val,
			["大于等于"]: (item, val) => item >= val,
			["小于等于"]: (item, val) => item <= val
		};
		let data = t.filter(item => {
			return Object.keys(searchdata).every(i => {
				let type = searchdata[i].type || "等于";
				let val = searchdata[i].val || searchdata[i];
				//let { type = "等于", val = searchval } = searchval;
				let fn = typefn[type];
				if (typeof val == "function") {
					return val(item[i])
				}
				return fn(item[i], val);
			});
		});
		if (Object.keys(searchdata).length == 0) {
			data = t;
		}
		return data;
	};

	/**
	 * 对象、数组变化监听(增删改)
	 * @author w-bing
	 * @date 2020-04-22
	 * @param {Object} obj
	 * @param {Function} func
	 * @return {Proxy}
	 */
	deepProxy = function (obj, func) {
		if (typeof obj === 'object') {
			for (let key in obj) {
				if (typeof obj[key] === 'object') {
					obj[key] = deepProxy(obj[key], func);
				}
			}
		}

		return new Proxy(obj, {
			/**
			 * @param {Object, Array} target 设置值的对象
			 * @param {String} key 属性
			 * @param {any} value 值
			 * @param {Object} receiver this
			 */
			set: function (target, key, value, receiver) {
				if (typeof value === 'object') {
					value = deepProxy(value, func);
				}

				let cbType = target[key] == undefined ? 'create' : 'modify';

				let r = Reflect.set(target, key, value, receiver);
				//排除数组修改length回调
				if (!(Array.isArray(target) && key === 'length')) {
					func.apply(this, [cbType, { target, key, value }]);
				}
				return r;
			},
			deleteProperty(target, key) {
				let r = Reflect.deleteProperty(target, key);
				func.apply(this, ['delete', { target, key }]);
				return r;
			}
		});
	}

	/*
	 * 复制到剪切板的方法
	 * try{await copyToClipboard(sql);}//成功
	 * catch (e){}//失败
	 * */
	copyToClipboard = function (textToCopy) {
		// navigator clipboard 需要https等安全上下文
		if (navigator.clipboard && window.isSecureContext) {
			// navigator clipboard 向剪贴板写文本
			return navigator.clipboard.writeText(textToCopy);
		} else {
			// 创建text area
			let textArea = document.createElement("textarea");
			textArea.value = textToCopy;
			// 使text area不在viewport，同时设置不可见
			textArea.style.position = "absolute";
			textArea.style.opacity = 0;
			textArea.style.left = "-999999px";
			textArea.style.top = "-999999px";
			document.body.appendChild(textArea);
			textArea.focus();
			textArea.select();
			return new Promise((res, rej) => {
				// 执行复制命令并移除文本框
				document.execCommand('copy') ? res() : rej();
				textArea.remove();
			});
		}
	}

	downloadFile = function (url, fileName) {
		let link = document.createElement('a');
		link.href = url;
		link.download = fileName;
		link.click();
	}
})($);