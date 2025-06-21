document.getElementById("createStudentForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const studentId = document.getElementById("studentId").value;
    const fullName = document.getElementById("fullName").value;
    const documentNumber = document.getElementById("documentNumber").value;
    const email = document.getElementById("email").value;

    const data = { fullName, documentNumber, email };

    const url = studentId
        ? `https://localhost:44309/api/school/students/${studentId}`
        : "https://localhost:44309/api/school/students";

    const method = studentId ? "PUT" : "POST";

    try {
        const response = await fetch(url, {
            method,
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data)
        });

        if (!response.ok) {
            const error = await response.text();
            throw new Error(error);
        }

        alert(studentId ? "Estudiante actualizado" : "Estudiante creado");
        location.reload();
    } catch (err) {
        alert("Error: " + err.message);
    }
});

function openEditModal(button) {
    const id = button.getAttribute("data-id");
    const name = button.getAttribute("data-name");
    const doc = button.getAttribute("data-doc");
    const email = button.getAttribute("data-email");

    document.getElementById("studentId").value = id;
    document.getElementById("fullName").value = name;
    document.getElementById("documentNumber").value = doc;
    document.getElementById("email").value = email;

    const modal = new bootstrap.Modal(document.getElementById('createStudentModal'));
    modal.show();
}

async function deleteStudent(studentId) {
    if (!confirm("¿Estás seguro de eliminar este estudiante?")) return;

    try {
        const response = await fetch(`https://localhost:44309/api/school/students/${studentId}`, {
            method: "DELETE"
        });

        if (!response.ok) {
            const error = await response.text();
            throw new Error(error);
        }

        alert("Estudiante eliminado correctamente");
        location.reload();
    } catch (err) {
        alert("Error al eliminar estudiante: " + err.message);
    }
}
