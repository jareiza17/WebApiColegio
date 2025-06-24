document.getElementById("subjectForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const subjectId = document.getElementById("subjectId").value;
    const name = document.getElementById("subjectName").value;
    const code = document.getElementById("subjectCode").value;
    const credits = parseInt(document.getElementById("subjectCredits").value);

    const data = { name, code, credits };

    const url = subjectId
        ? `https://localhost:7148/api/school/subjects/${subjectId}`
        : "https://localhost:7148/api/school/subjects";

    const method = subjectId ? "PUT" : "POST";

    try {
        const response = await fetch(url, {
            method,
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data)
        });

        if (!response.ok) throw new Error(await response.text());

        alert("Materia guardada correctamente");
        location.reload();
    } catch (err) {
        alert("Error al guardar la materia: " + err.message);
    }
});

function openEditSubjectModal(button) {
    const id = button.getAttribute("data-id");
    const name = button.getAttribute("data-name");
    const code = button.getAttribute("data-code");
    const credits = button.getAttribute("data-credits");

    document.getElementById("subjectId").value = id;
    document.getElementById("subjectName").value = name;
    document.getElementById("subjectCode").value = code;
    document.getElementById("subjectCredits").value = credits;

    const modal = new bootstrap.Modal(document.getElementById('subjectModal'));
    modal.show();
}

async function deleteSubject(subjectId) {
    if (!confirm("¿Estás seguro de eliminar esta materia?")) return;

    try {
        const response = await fetch(`https://localhost:7148/api/school/subjects/${subjectId}`, {
            method: "DELETE"
        });

        if (!response.ok) throw new Error(await response.text());

        alert("Materia eliminada correctamente");
        location.reload();
    } catch (err) {
        alert("Error al eliminar la materia: " + err.message);
    }
}
