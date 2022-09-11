class Repository {
    constructor(controller) {
        this.controller = controller;
    }

    httpGet(action, params) {
        return new Promise((resolve, reject) => {
            $.ajax({
                method: 'GET',
                url: `/${this.controller}/${action}`,
                data: params,
                dataType: 'JSON',
                success: resolve,
                error: reject
            });
        });
    };

    httpPost(action, params) {
        return new Promise((resolve, reject) => {
            $.ajax({
                method: 'POST',
                url: `/${this.controller}/${action}`,
                data: params,
                dataType: 'JSON',
                success: resolve,
                error: reject
            });
        });
    }
}