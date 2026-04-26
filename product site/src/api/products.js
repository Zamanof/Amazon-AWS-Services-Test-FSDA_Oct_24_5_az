import {API_BASE} from "../config.js";

async function readErrorMessage(response) {
    const text = await response.text();
    if(!text)
        return response.statusText || `HTTP ${response.status}`;

    try {
        const json = JSON.parse(text);
        if (typeof json === "string") return json;
        if(json.message) return json.message;
        if(json.title) return json.title;

        if(json.errors && json.errors ==='object') {
            const parts = []
            for(const [, msg] of Object.entries(json.errors)) {
                if(Array.isArray(msg)) parts.push(...msg);
                else parts.push(String(msg));
            }
            if (parts.length) return parts.join(' ');
        }
        return text;
    }
    catch {
        return text;
    }
}

export async function getAllProducts(){
    const response = await fetch(`${API_BASE}/products`);
    if(!response.ok)
        throw new Error(await readErrorMessage(response));
    return response.json();
}

export async function getProductById(id){
    const response = await fetch(`${API_BASE}/products/${id}`);
    if(!response.ok)
        throw new Error(await readErrorMessage(response));
    return response.json();
}

export async function createProduct(formData){
    const response =
        await fetch(`${API_BASE}/products`, {
        method: 'POST',
        body: formData,
    })
    if(!response.ok)
        throw new Error(await readErrorMessage(response));
    return response.json();
}

export async function updateProduct(id, formData){
    const response = await fetch(`${API_BASE}/products/${id}`, {
        method: 'PUT',
        body: formData,
    })
    if(!response.ok)
        throw new Error(await readErrorMessage(response));
    return response.json();
}

export async function deleteProduct(id){
    const response = await fetch(`${API_BASE}/products/${id}`, {
        method: 'DELETE',
    });
    if(!response.ok && response.status !== 204)
        throw new Error(await readErrorMessage(response));

}